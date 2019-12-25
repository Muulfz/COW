using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Onset.Plugin;

namespace Onset.Runtime.Plugin
{
    internal class Payload
    {
        public string File { get; }

        public Assembly Assembly { get; }

        public Meta Meta { get; }

        public Type PluginType { get; }

        public List<string> Dependencies { get; }

        internal Payload(string file, Assembly assembly, Meta meta, Type pluginType)
        {
            File = file;
            Assembly = assembly;
            Meta = meta;
            PluginType = pluginType;
            Dependencies = meta.Dependencies.ToList();
        }

        internal OnsetPlugin CreatePlugin()
        {
            OnsetPlugin plugin = (OnsetPlugin) Activator.CreateInstance(PluginType);
            plugin.Meta = Meta;
            plugin.Server = Wrapper.Server;
            plugin.Logger = new Logger(Meta.Name, Meta.IsDebug);
            return plugin;
        }
    }
}
