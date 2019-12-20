using System;
using Onset.Plugin;
using Onset.Runtime.Plugin;

namespace Onset.Runtime
{
    [Serializable]
    internal class Server : IServer
    {
        public IPluginManager PluginManager { get; }

        public ILogger Logger { get; }

        internal Server()
        {
            Logger = new Logger("C#");
            PluginManager = new PluginManager();
        }

        internal void Start()
        {
            ((PluginManager) PluginManager).LoadPlugins();
        }

        internal void Stop()
        {
            foreach (OnsetPlugin plugin in PluginManager.Plugins)
            {
                PluginManager.StopPlugin(plugin);
            }
        }
    }
}
