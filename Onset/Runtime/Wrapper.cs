using Newtonsoft.Json;
using Onset.Converter;
using Onset.Entities;
using Onset.Event;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using Onset.Enums;
using Onset.Interop;

namespace Onset.Runtime
{
    internal class Wrapper
    {
        internal const string RuntimeName = "COW.Runtime.dll";
        internal const int ApiVersion = 1;

        internal static Server Server { get; private set; }

        private static readonly Logger Logger = new Logger("Wrapper", true);


        [DllImport(RuntimeName, EntryPoint = "execute_lua", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ExecuteLuaPtr([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string data);

        internal static ReturnData ExecuteLua(string name, object data = null)
        {
            string json = "";
            if (data != null)
            {
                json = Escape(data);
            }
            return new ReturnData(Marshal.PtrToStringUTF8(ExecuteLuaPtr(name, json)));
        }

        [DllImport(RuntimeName, EntryPoint = "log_to_console", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LogConsole([MarshalAs(UnmanagedType.LPStr)]string message);

        internal static bool ExecuteEvent(string name, string json)
        {
            try
            {
                if (name == "finish-wrapper")
                {
                    Server.Start();
                    Server.Logger.Success("Finish-Trigger received! Wrapper has been finished and is now completely functional!");
                    return false;
                }

                if (name == "trigger-event")
                {
                    return Server.ExecuteServerEvent(new ReturnData(json));
                }

                if (name == "interop-luaexp")
                {
                    LuaInterop.CallInterop(json);
                }

                if (name == "trigger-remote-event")
                {
                    ReturnData data = new ReturnData(json);
                    string[] args = data.ValuesAsStrings("args");
                    IPlayer player = Server.PlayerPool.GetEntity(data.Value<int>("player"));
                    string eventName = data.Value<string>("eventName");
                    Registry<RemoteEvent>.Item remoteItem = Server.RemoteEventRegistry.GetItem(item => item.Data.Key == eventName);
                    if (remoteItem == null) return false;
                    remoteItem.Invoke(Converts.Convert(args, remoteItem.Invoker.GetParameters(), player, false, remoteItem.Invoker.Name + " -> " + remoteItem.Data.Key));
                }

                if (name == "trigger-command")
                {
                    ReturnData data = new ReturnData(json);
                    string[] args = data.ValuesAsStrings("args");
                    IPlayer player = Server.PlayerPool.GetEntity(data.Value<int>("player"));
                    Registry<Command>.Item commandItem =
                        Server.CommandRegistry.GetItem(item => item.Data.Name == data.Value<string>("commandName"));
                    if (commandItem == null)
                    {
                        Server.ExecuteInternalServerEvent(EventType.PlayerCommandFailed, player,
                            data.Value<string>("commandName"), args, CommandError.NotExisting);
                        return false;
                    }

                    if (Server.ExecuteInternalServerEvent(EventType.PlayerPreCommand, player, data.Value<string>("commandName"), args))
                    {
                        int requiredParams = commandItem.Invoker.GetParameters().Count(info => !info.IsOptional) - 1;
                        if (requiredParams > args.Length)
                        {
                            Server.ExecuteInternalServerEvent(EventType.PlayerCommandFailed, player,
                                data.Value<string>("commandName"), args, CommandError.TooFewArguments);
                            return false;
                        }

                        object[] arr = Converts.Convert(args, commandItem.Invoker.GetParameters(), player, true,
                            "Command");
                        if (arr == null) return false;
                        commandItem.Invoke(arr);
                    }
                }
            }
            catch (Exception e)
            {
                Server.Logger.Error("An unhandled exception occurred!", e);
            }
            return false;
        }

        internal static void Load()
        {
            Logger.Info("Loading Wrapper...");
            Server = new Server();
            Logger.Success("Wrapper loaded!");
        }

        internal static void Unload()
        {
            Logger.Warn("Stopping Wrapper...");
            Server.Stop();
            Logger.Success("COW: Wrapper stopped");
        }

        internal static string Escape(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        internal static IEntity GetEntityByAttach(long entity, AttachType type)
        {
            switch (type)
            {
                case AttachType.None:
                    return null;
                case AttachType.Player:
                    return Server.PlayerPool.GetEntity(entity);
                case AttachType.Vehicle:
                    return null; //TODO
                case AttachType.Object:
                    return Server.ObjectPool.GetEntity(entity);
                case AttachType.NPC:
                    return Server.NPCPool.GetEntity(entity);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal static AttachType GetAttachType(IEntity entity)
        {
            if (entity is IPlayer)
            {
                return AttachType.Player;
            }

            if (entity is INPC)
            {
                return AttachType.NPC;
            }

            if (entity is IVehicle)
            {
                return AttachType.Vehicle;
            }

            if (entity is IObject)
            {
                return AttachType.Object;
            }

            return AttachType.None;
        }
    }
}
