using Onset.Dimension;
using Onset.Entities;
using Onset.Event;
using Onset.Helper;
using Onset.Plugin;
using Onset.Runtime.Entities;
using Onset.Runtime.Plugin;
using Onset.Runtime.Pools;
using Onset.Utils;
using System;
using System.Collections.Generic;

namespace Onset.Runtime
{
    internal class Server : IServer
    {
        public IPluginManager PluginManager { get; }

        public ILogger Logger { get; }

        public int GameVersion => Wrapper.ExecuteLua("COW_GetGameVersion").Value<int>("version");

        public List<IPlayer> AllPlayers => PlayerPool.Entities;

        public List<IDoor> AllDoors => DoorPool.Entities;

        public List<INPC> AllNPCs => NPCPool.Entities;

        public List<IPickup> AllPickups => PickupPool.Entities;

        public List<string> AllPackages => Wrapper.ExecuteLua("COW_GetAllPackages").Value<List<string>>("packages");

        public float TickRate => Wrapper.ExecuteLua("COW_GetServerTickRate").Value<float>("val");

        public string Name
        {
            get => Wrapper.ExecuteLua("COW_GetServerName").Value<string>("name");
            set => Wrapper.ExecuteLua("COW_SetServerName", new { name = value });
        }

        public int MaxPlayers => Wrapper.ExecuteLua("COW_GetMaxPlayers").Value<int>("val");

        public IDimension Global { get; }

        internal Registry<Command> CommandRegistry { get; }

        internal Registry<RemoteEvent> RemoteEventRegistry { get; }

        internal Registry<ServerEvent> ServerEventRegistry { get; }

        internal EntityPool<IPlayer> PlayerPool { get; }

        internal EntityPool<IDoor> DoorPool { get; }

        internal EntityPool<IPickup> PickupPool { get; }

        internal EntityPool<INPC> NPCPool { get; }

        internal List<IDimension> DimensionPool { get; }

        public string GameVersionAsString => Wrapper.ExecuteLua("COW_GetGameVersionString").Value<string>("version");

        internal Server()
        {
            Logger = new Logger(null, true);
            Global = new Dimension(0);
            DimensionPool = new List<IDimension> { Global };
            PluginManager = new PluginManager(this);
            ServerEventRegistry = new Registry<ServerEvent>();
            RemoteEventRegistry = new Registry<RemoteEvent>();
            RemoteEventRegistry.ItemRegistered += item =>
            {
                Wrapper.ExecuteLua("COW_AddRemoteEvent", new { eventName = item.Data.Key });
            };
            CommandRegistry = new Registry<Command>();
            CommandRegistry.ItemRegistered += item =>
            {
                Wrapper.ExecuteLua("COW_AddCommand", new { commandName = item.Data.Name });
            };
            PlayerPool = new EntityPool<IPlayer>(id => new Player(id));
            DoorPool = new EntityPool<IDoor>(id => new Door(id));
            NPCPool = new EntityPool<INPC>(id => new NPC(id));
            PickupPool = new EntityPool<IPickup>(id => new Pickup(id));
        }

        public IDimension GetDimension(uint id)
        {
            return DimensionPool.SelectFirst(dimension => dimension.ID == id, () =>
            {
                Dimension dimension = new Dimension(id);
                DimensionPool.Add(dimension);
                return dimension;
            });
        }

        internal void Start()
        {
            long pingTime = Time.CurrentTimeMillis();
            string gameVersion = GameVersionAsString;
            long currentTime = Time.CurrentTimeMillis();
            Logger.Debug("Ping test took " + (currentTime - pingTime) + " millis!");
            Logger.Info("Found game version: " + gameVersion);
            ((PluginManager)PluginManager).LoadPlugins();
        }

        internal void Stop()
        {
            PluginManager.Plugins.SafeForEach(plugin => PluginManager.StopPlugin(plugin));
        }

        public void Exit(string reason = "")
        {
            Wrapper.ExecuteLua("COW_ServerExit", new { reason });
        }

        public void StartPackage(string packageName)
        {
            Wrapper.ExecuteLua("COW_StartPackage", new { name = packageName });
        }

        public void StopPackage(string packageName)
        {
            Wrapper.ExecuteLua("COW_StopPackage", new { name = packageName });
        }

        public bool IsPackageStarted(string packageName)
        {
            return Wrapper.ExecuteLua("COW_IsPackageStarted", new { name = packageName }).Value<bool>("state");
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
                EventType type = (EventType)data.Value<int>("type");
                object[] args;
                IPlayer associatedPlayer = type.IsPlayerEvent() ? PlayerPool.GetEntity(data.Value<int>("player")) : null;
                switch (type)
                {
                    case EventType.PlayerQuit:
                        args = new object[] { associatedPlayer };
                        break;
                    case EventType.PlayerChat:
                        string pcText = data.Value<string>("text");
                        args = new object[] { associatedPlayer, pcText };
                        break;
                    case EventType.PlayerChatCommand:
                        args = new object[] { associatedPlayer, data.Value<string>("command"), data.Value<bool>("exists") };
                        break;
                    case EventType.PlayerJoin:
                        args = new object[] { associatedPlayer };
                        break;
                    default:
                        args = new object[0];
                        break;
                }

                bool cancel = false;
                foreach (Registry<ServerEvent>.Item item in ServerEventRegistry.GetAll(item => item.Data.Type == type))
                {
                    object @return = item.Invoke(args);
                    if (@return != null && (bool)@return)
                    {
                        cancel = true;
                    }
                }

                if (type == EventType.PlayerQuit)
                {
                    if (!associatedPlayer.IsValid)
                    {
                        Logger.Debug($"Cleaner got kicked off for player ({associatedPlayer.ID}) on quit!");
                    }
                }

                return cancel;
            }
            catch (Exception e)
            {
                Logger.Error("Tried to execute a server event but couldn't", e);
                return false;
            }
        }
    }
}
