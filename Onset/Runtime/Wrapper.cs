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


        /*[DllImport("Onset.Runtime.dll", EntryPoint = "execute_lua")] //maybe change name?
        internal static extern string ExecuteLua(string name, bool simple, string data);*/

        [DllImport(RuntimeName, EntryPoint = "execute_lua", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ExecuteLuaPtr([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string data);

        internal static string ExecuteLUA(string name, string data = "")
        {
            return Marshal.PtrToStringUTF8(ExecuteLuaPtr(name, data));
        }

        [DllImport(RuntimeName, EntryPoint = "log_to_console", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LogConsole([MarshalAs(UnmanagedType.LPStr)]string message);

        internal static bool ExecuteEvent(string name, string data)
        {
            if (name == "finish-wrapper")
            {
                Server.Start();
                LogConsole("COW: Finish-Trigger received! Wrapper has been finished and is now completely functional!");
                LogConsole("COW: Test Ping?: " + ExecuteLUA("OnTest"));
                return false;
            }
            return false;
        }

        internal static void Load()
        {
            LogConsole("COW: Loading Wrapper...");
            Server = new Server();
            Server.Start();
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

        private class ConstCharPtrMarshaler : ICustomMarshaler
        {
            public object MarshalNativeToManaged(IntPtr pNativeData)
            {
                return Marshal.PtrToStringAnsi(pNativeData);
            }

            public IntPtr MarshalManagedToNative(object ManagedObj)
            {
                return IntPtr.Zero;
            }

            public void CleanUpNativeData(IntPtr pNativeData)
            {
            }

            public void CleanUpManagedData(object ManagedObj)
            {
            }

            public int GetNativeDataSize()
            {
                return IntPtr.Size;
            }

            static readonly ConstCharPtrMarshaler instance = new ConstCharPtrMarshaler();

            public static ICustomMarshaler GetInstance(string cookie)
            {
                return instance;
            }
        }
    }
}
