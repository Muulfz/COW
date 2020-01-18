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
using System.Threading.Tasks;
using Onset.Enums;

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

        public List<IText3D> AllText3Ds => Text3DPool.Entities;

        public List<IObject> AllObjects => ObjectPool.Entities;

        public List<IVehicle> AllVehicles => VehiclePool.Entities;

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

        public EntityPool<IObject> ObjectPool { get; }

        internal EntityPool<IText3D> Text3DPool { get; }

        internal EntityPool<IVehicle> VehiclePool { get; }

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
            Text3DPool = new EntityPool<IText3D>(id => new Text3D(id));
            ObjectPool = new EntityPool<IObject>(id => new Entities.Object(id));
            VehiclePool = new EntityPool<IVehicle>(id => new Vehicle(id));
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

        public void Broadcast(string message)
        {
            Multicast(message, AllPlayers);
        }

        public void Broadcast(string message, Vector position, float range)
        {
            foreach (IPlayer player in AllPlayers)
            {
                if (player.Position.DistanceTo(position) <= range)
                {
                    player.SendMessage(message);
                }
            }
        }

        public void Multicast(string message, IEnumerable<IPlayer> players)
        {
            foreach (IPlayer player in players)
            {
                player.SendMessage(message);
            }
        }

        public void Multicast(string message, params IPlayer[] players)
        {
            foreach (IPlayer player in players)
            {
                player.SendMessage(message);
            }
        }

        public void ExecuteTask(Action task)
        {
            Task.Run(task);
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
                    case EventType.PlayerPickupHit:
                        args = new object[] { associatedPlayer, PickupPool.GetEntity(data.Value<long>("pickup")) };
                        break;
                    case EventType.PackageStart:
                        args = new object[0];
                        break;
                    case EventType.PackageStop:
                        args = new object[0];
                        break;
                    case EventType.GameTick:
                        args = new object[] { data.Value<float>("delta") };
                        break;
                    case EventType.ClientConnectionRequest:
                        args = new object[] { data.Value<string>("ip"), data.Value<int>("port") };
                        break;
                    case EventType.NPCReachTarget:
                        args = new object[] { NPCPool.GetEntity(data.Value<long>("npc")) };
                        break;
                    case EventType.NPCDamage:
                        args = new object[] { NPCPool.GetEntity(data.Value<long>("npc")), (DamageType)data.Value<int>("damagetype"), data.Value<int>("amount") };
                        break;
                    case EventType.NPCSpawn:
                        args = new object[] { NPCPool.GetEntity(data.Value<long>("npc")) };
                        break;
                    case EventType.NPCDeath:
                        args = new object[] { NPCPool.GetEntity(data.Value<long>("npc")) };
                        break;
                    case EventType.NPCStreamIn:
                        args = new object[] { associatedPlayer, NPCPool.GetEntity(data.Value<long>("npc")) };
                        break;
                    case EventType.NPCStreamOut:
                        args = new object[] { associatedPlayer, NPCPool.GetEntity(data.Value<long>("npc")) };
                        break;
                    case EventType.PlayerEnterVehicle:
                        args = new object[] { associatedPlayer, VehiclePool.GetEntity(data.Value<long>("vehicle")), data.Value<int>("seat") };
                        break;
                    case EventType.PlayerLeaveVehicle:
                        args = new object[] { associatedPlayer, VehiclePool.GetEntity(data.Value<long>("vehicle")), data.Value<int>("seat") };
                        break;
                    case EventType.PlayerStateChange:
                        args = new object[] { associatedPlayer, (PlayerState) data.Value<int>("newstate"), (PlayerState)data.Value<int>("oldstate") };
                        break;
                    case EventType.VehicleRespawn:
                        args = new object[] { VehiclePool.GetEntity(data.Value<long>("vehicle")) };
                        break;
                    case EventType.VehicleStreamIn:
                        args = new object[] { associatedPlayer, VehiclePool.GetEntity(data.Value<long>("vehicle")) };
                        break;
                    case EventType.VehicleStreamOut:
                        args = new object[] { associatedPlayer, VehiclePool.GetEntity(data.Value<long>("vehicle")) };
                        break;
                    case EventType.PlayerServerAuth:
                        args = new object[] { associatedPlayer };
                        break;
                    case EventType.PlayerSteamAuth:
                        args = new object[] { associatedPlayer };
                        break;
                    case EventType.PlayerDownloadFile:
                        args = new object[] { associatedPlayer, data.Value<string>("file"), data.Value<string>("checksum") };
                        break;
                    case EventType.PlayerStreamIn:
                        args = new object[] { associatedPlayer, PlayerPool.GetEntity(data.Value<long>("other")) };
                        break;
                    case EventType.PlayerStreamOut:
                        args = new object[] { associatedPlayer, PlayerPool.GetEntity(data.Value<long>("other")) };
                        break;
                    case EventType.PlayerSpawn:
                        args = new object[] { associatedPlayer };
                        break;
                    case EventType.PlayerDeath:
                        args = new object[] { associatedPlayer, PlayerPool.GetEntity(data.Value<long>("killer")) };
                        break;
                    case EventType.PlayerWeaponShot:
                        HitType hitType = (HitType) data.Value<int>("hittype");
                        args = new object[]
                        {
                            associatedPlayer, (Weapon) data.Value<int>("weapon"), hitType,
                            GetEntityByHitType(hitType, data.Value<long>("entity")), data.ExtractPosition("hit"),
                            data.ExtractPosition("start"), data.ExtractPosition("normal")
                        };
                        break;
                    case EventType.PlayerDamage:
                        args = new object[] { associatedPlayer, (DamageType) data.Value<int>("damagetype"), data.Value<int>("amount") };
                        break;
                    case EventType.PlayerInteractDoor:
                        args = new object[] { associatedPlayer, DoorPool.GetEntity(data.Value<long>("door")), data.Value<bool>("state") };
                        break;
                    default:
                        args = new object[0];
                        break;
                }

                bool cancel = true;
                foreach (Registry<ServerEvent>.Item item in ServerEventRegistry.GetAll(item => item.Data.Type == type))
                {
                    object @return = item.Invoke(args);
                    if (@return != null && !(bool)@return)
                    {
                        cancel = false;
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

        private IEntity GetEntityByHitType(HitType type, long id)
        {
            switch (type)
            {
                case HitType.Air:
                    return null;
                case HitType.Player:
                    return PlayerPool.GetEntity(id);
                case HitType.Vehicle:
                    return VehiclePool.GetEntity(id);
                case HitType.NPC:
                    return NPCPool.GetEntity(id);
                case HitType.Object:
                    return ObjectPool.GetEntity(id);
                case HitType.Landscape:
                    return null;
                case HitType.Water:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
