using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace Onset.Runtime
{
    internal class Wrapper
    {
        internal const string RuntimeName = "COW.Runtime.dll";
        internal const int ApiVersion = 1;

        internal static Server Server { get; private set; }


        [DllImport(RuntimeName, EntryPoint = "execute_lua", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ExecuteLuaPtr([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string data);

        internal static ReturnData ExecuteLUA(string name, string data = "")
        {
            return new ReturnData(Marshal.PtrToStringUTF8(ExecuteLuaPtr(name, data)));
        }

        [DllImport(RuntimeName, EntryPoint = "log_to_console", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LogConsole([MarshalAs(UnmanagedType.LPStr)]string message);

        internal static bool ExecuteEvent(string name, string data)
        {
            if (name == "finish-wrapper")
            {
                Server.Start();
                LogConsole("COW: Finish-Trigger received! Wrapper has been finished and is now completely functional!");
                return false;
            }
            return false;
        }

        internal static void Load()
        {
            LogConsole("COW: Loading Wrapper...");
            Server = new Server();
            LogConsole("COW: Wrapper loaded!");
        }

        internal static void Unload()
        {
            LogConsole("COW: Stopping Wrapper...");
            Server.Stop();
            LogConsole("COW: Wrapper stopped");
        }

        internal static string Escape(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
