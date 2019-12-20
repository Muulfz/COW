using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Onset.Helper;
using Onset.Plugin;
// ReSharper disable ConvertToUsingDeclaration

namespace Onset.Runtime.Plugin
{
    internal class PluginManager : IPluginManager
    {
        private readonly Type _pluginType = typeof(OnsetPlugin);

        public List<OnsetPlugin> Plugins { get; }

        public string PluginsPath { get; }

        internal PluginManager()
        {
            Plugins = new List<OnsetPlugin>();
            PluginsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "plugins");
            if (!Directory.Exists(PluginsPath))
            {
                Directory.CreateDirectory(PluginsPath);
            }
        }

        internal void LoadPlugins()
        {
            string[] files = Directory.GetFiles(PluginsPath);
            if (files == null) return;
            foreach (string file in files)
            {
                if (!Path.GetExtension(file).ToLower().Contains("dll")) continue;
                Payload result = LoadPayload(file);
                if (result == null)
                {
                    //Native.getServer().getLogger().warn("Could not extract classes from \"" + file.getAbsolutePath() + "\"");
                    continue;
                }

                if (GetPlugin(result.Meta.ID) != null)
                {
                    //Error!
                    return;
                }
                StartPlugin(result.CreatePlugin());
            }/*
            while (plugins.Count > 0)
            {
                bool missingDependency = true;
                Iterator<Tuple<string, Payload>> pluginIterator = plugins.Entries().Iterator();
                while (pluginIterator.HasNext())
                {
                    Tuple<string, Payload> entry = pluginIterator.Next();
                    string plugin = entry.Item1;
                    if (dependencies.ContainsKey(plugin))
                    {
                        Iterator<string> dependencyIterator = dependencies[plugin].Iterator();
                        while (dependencyIterator.HasNext())
                        {
                            string dependency = dependencyIterator.Next();
                            if (dependency == null) break;
                            if (loadedPlugins.Contains(dependency))
                            {
                                dependencyIterator.Remove();
                            }
                            else if (!plugins.ContainsKey(dependency))
                            {
                                missingDependency = false;
                                pluginIterator.Remove();
                                dependencies.Remove(plugin);
                                //Native.getServer().getLogger().fatal("Could not load '" + entry.getValue().getMeta().id() + "' because of a missing dependency!");
                            }
                        }

                        if (dependencies.ContainsKey(plugin) && dependencies[plugin].Count == 0)
                        {
                            dependencies.Remove(plugin);
                        }
                    }
                    if (!dependencies.ContainsKey(plugin) && plugins.ContainsKey(plugin))
                    {
                        Payload result = plugins[plugin];
                        pluginIterator.Remove();
                        missingDependency = false;
                        try
                        {
                            StartPlugin(result.CreatePlugin());
                            loadedPlugins.Add(plugin);
                        }
                        catch (Exception ex)
                        {
                            //Native.getServer().getLogger().error("Could not load plugin '" + result.getMeta().id() + "'!", ex);
                        }
                    }
                }
                if (missingDependency)
                {
                    pluginIterator = plugins.Entries().Iterator();
                    while (pluginIterator.HasNext())
                    {
                        Tuple<string, Payload> entry = pluginIterator.Next();
                        string plugin = entry.Item1;
                        if (!dependencies.ContainsKey(plugin))
                        {
                            missingDependency = false;
                            Payload result = entry.Item2;
                            pluginIterator.Remove();

                            try
                            {
                                StartPlugin(result.CreatePlugin());
                                loadedPlugins.Add(plugin);
                                break;
                            }
                            catch (Exception ex)
                            {
                                //Native.getServer().getLogger().error("Could not load plugin '" + result.getMeta().id() + "'!", ex);
                            }
                        }
                    }
                    if (missingDependency)
                    {
                        dependencies.Clear();
                        Iterator<Payload> failedPluginIterator = plugins.Values.ToList().Iterator();
                        while (failedPluginIterator.HasNext())
                        {
                            Payload file = failedPluginIterator.Next();
                            if (file == null) break;
                            failedPluginIterator.Remove();
                            //Native.getServer().getLogger().fatal("Could not load plugin '" + file.getMeta().id() + "': circular dependency detected");
                        }
                    }
                }
            }*/
        }

        public OnsetPlugin GetPlugin(string id)
        {
            return GetPlugin(plugin => plugin.Meta.ID == id);
        }

        public OnsetPlugin GetPlugin(Predicate<OnsetPlugin> select)
        {
            return Plugins.SelectFirst(select);
        }

        public void StartPlugin(OnsetPlugin plugin)
        {
            try
            {
                plugin.State = PluginState.Enabling;
                plugin.Load();
                Plugins.Add(plugin);
                plugin.State = PluginState.Enabled;
            }
            catch
            {
                plugin.State = PluginState.Failed;
            }
        }

        public void StopPlugin(OnsetPlugin plugin)
        {
            try
            {
                plugin.State = PluginState.Disabling;
                plugin.Unload();
                Plugins.Remove(plugin);
                plugin.State = PluginState.Disabled;
            }
            catch
            {
                plugin.State = PluginState.Failed;
            }
        }

        private bool TryLoadLibrary(string file)
        {
            try
            {
                Assembly.LoadFrom(file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Payload LoadPayload(string file)
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(file);
                Type pluginType = assembly.GetExportedTypes().SelectFirst(type => _pluginType.IsAssignableFrom(type));
                Meta meta = pluginType.GetCustomAttribute<Meta>();
                if (meta.ApiVersion < Wrapper.ApiVersion) return null;
                return new Payload(file, assembly, meta, pluginType);
            }
            catch
            {
                //Ignore
            }
            TryLoadLibrary(file);
            return null;
        }
    }
}
