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

        private readonly Server _server;

        internal PluginManager(Server server)
        {
            _server = server;
            Plugins = new List<OnsetPlugin>();
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            PluginsPath = Path.Combine(currentPath, "plugins");
            foreach (string file in Directory.GetFiles(currentPath))
            {
                if (Path.GetExtension(file).ToLower().Contains("dll") && Path.GetFileNameWithoutExtension(file).ToLower() != "Onset")
                {
                    TryLoadLibrary(file);
                }
            }
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
                    Wrapper.Server.Logger.Warn("Could not load DLL \"" + Path.GetFileName(file) + "\"! Maybe it was a Library?!");
                    continue;
                }

                if (GetPlugin(result.Meta.ID) != null)
                {
                    Wrapper.Server.Logger.Warn("Found a Plugin with the ID \"" + result.Meta.ID + "\" but there is Plugin loaded with the same ID!");
                    return;
                }
                Wrapper.Server.Logger.Info("Found and created Plugin: \"" + result.Meta.ID + "\"! Starting it...");
                StartPlugin(result.CreatePlugin());
            }
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
                _server.CommandRegistry.Register(plugin);
                plugin.State = PluginState.Enabled;
                plugin.Logger.Success("Loaded Plugin successfully!");
                plugin.Logger.Warn("This plugin is in DEBUG mode!");
            }
            catch(Exception e)
            {
                Wrapper.Server.Logger.Error("An error occurred while starting Plugin \"" + plugin.Meta.ID + "\"!", e);
                plugin.State = PluginState.Failed;
            }
        }

        public void StopPlugin(OnsetPlugin plugin)
        {
            try
            {
                plugin.Logger.Info("Disabling this Plugin...");
                plugin.State = PluginState.Disabling;
                plugin.Unload();
                Plugins.Remove(plugin);
                plugin.State = PluginState.Disabled;
                plugin.Logger.Success("Plugin disabled!");
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
