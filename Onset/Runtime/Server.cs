using System;
using System.Collections.Generic;
using Onset.Entities;
using Onset.Event;
using Onset.Helper;
using Onset.Plugin;
using Onset.Runtime.Plugin;
using Onset.Runtime.Pools;

namespace Onset.Runtime
{
    internal class Server : IServer
    {
        public IPluginManager PluginManager { get; }

        public ILogger Logger { get; }

        public int GameVersion => Wrapper.ExecuteLua("COW_GetGameVersion").Value<int>("version");

        public List<IPlayer> Players => PlayerPool.Players;

        internal Registry<Command> CommandRegistry { get; }

        internal Registry<RemoteEvent> RemoteEventRegistry { get; }

        internal Registry<ServerEvent> ServerEventRegistry { get; }

        internal PlayerPool PlayerPool { get; }

        public string GameVersionAsString => Wrapper.ExecuteLua("COW_GetGameVersionString").Value<string>("version");

        internal Server()
        {
            Logger = new Logger();
            PluginManager = new PluginManager(this);
            ServerEventRegistry = new Registry<ServerEvent>();
            RemoteEventRegistry = new Registry<RemoteEvent>();
            CommandRegistry = new Registry<Command>();
            PlayerPool = new PlayerPool();
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

        public void RegisterCommands(object obj)
        {
            CommandRegistry.Register(obj);
        }

        public void RegisterCommands<T>()
        {
            CommandRegistry.Register<T>();
        }

        public void RegisterEvents(object obj)
        {
            ServerEventRegistry.Register(obj);
        }

        public void RegisterEvents<T>()
        {
            ServerEventRegistry.Register<T>();
        }

        public void RegisterClientEvents(object obj)
        {
            RemoteEventRegistry.Register(obj);
        }

        public void RegisterClientEvents<T>()
        {
            RemoteEventRegistry.Register<T>();
        }

        internal bool ExecuteServerEvent(ReturnData data)
        {
            try
            {
                EventType type = (EventType) data.Value<int>("type");
                object[] args;
                IPlayer associatedPlayer = type.IsPlayerEvent() ? PlayerPool.GetPlayer(data.Value<int>("player")) : null;
                switch (type)
                {
                    case EventType.PlayerQuit:
                        args = new object[] { associatedPlayer };
                        break;
                    case EventType.PlayerChat:
                        string pcText = data.Value<string>("text");
                        if (ExecuteCommand(associatedPlayer, pcText)) return true;
                        args = new object[] { associatedPlayer, pcText };
                        break;
                    default:
                        args = new object[0];
                        break;
                }

                bool cancel = false;
                foreach (Registry<ServerEvent>.Item item in ServerEventRegistry.GetAll(item => item.Data.Type == type))
                {
                    object @return = item.Invoke(args);
                    if (@return != null && (bool) @return)
                    {
                        cancel = true;
                    }
                }

                if (type == EventType.PlayerQuit)
                {
                    PlayerPool.RemovePlayer(associatedPlayer);
                }

                return cancel;
            }
            catch(Exception e)
            {
                Logger.Error("Tried to execute a server event but couldn't", e);
                return false;
            }
        }

        private bool ExecuteCommand(IPlayer player, string text)
        {
            return false;
        }
    }
}
