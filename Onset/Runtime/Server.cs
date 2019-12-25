using System;
using Onset.Helper;
using Onset.Plugin;
using Onset.Runtime.Plugin;

namespace Onset.Runtime
{
    [Serializable]
    internal class Server : IServer
    {
        public IPluginManager PluginManager { get; }

        public ILogger Logger { get; }

        public int GameVersion => Wrapper.ExecuteLUA("COW_GetGameVersion").Value<int>("version");

        public string GameVersionAsString => Wrapper.ExecuteLUA("COW_GetGameVersionString").Value<string>("version");

        internal Server()
        {
            Logger = new Logger();
            PluginManager = new PluginManager();
        }

        internal void Start()
        {
            Logger.Info("Found game version: " + GameVersionAsString);
            ((PluginManager) PluginManager).LoadPlugins();
        }

        internal void Stop()
        {
            PluginManager.Plugins.SafeForEach(plugin => PluginManager.StopPlugin(plugin));
        }
    }
}
