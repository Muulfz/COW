using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Onset.Convertation;
using Onset.Entities;
using Onset.Event;

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
                        //TODO error handling: No command found
                        Server.Logger.Warn("Tried to execute command \"" + data.Value<string>("commandName") + "\": It is not a command");
                        return false;
                    }
                    //TODO permission handling
                    int requiredParams = commandItem.Invoker.GetParameters().Count(info => !info.IsOptional) - 1;
                    if (requiredParams > args.Length)
                    {
                        //TODO error handling: To few arguments!
                        Server.Logger.Warn("Tried to execute command \"" + commandItem.Data.Name + "\": To few arguments (got: " + args.Length + "; needed: " + requiredParams + ")");
                        return false;
                    }
                    object[] arr = Converts.Convert(args, commandItem.Invoker.GetParameters(), player, true, "Command");
                    if (arr == null) return false;
                    commandItem.Invoke(arr);
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
    }
}
