using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace Onset.Runtime
{
    internal class Wrapper
    {
        internal const int ApiVersion = 1;

        internal static Server Server { get; private set; }


        /*[DllImport("Onset.Runtime.dll", EntryPoint = "execute_lua")] //maybe change name?
        internal static extern string ExecuteLua(string name, bool simple, string data);*/

        [DllImport("Onset.Runtime.dll", EntryPoint = "execute_lua")]
        internal static extern string ExecuteLUA(string name, string data);

        internal static bool ExecuteEvent(string name, string data)
        {
            return false;
        }

        internal static void Load()
        {
            Server = new Server();
            Server.Start();
        }

        internal static void Unload()
        {
            Server.Stop();
        }

        internal static string Escape(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
