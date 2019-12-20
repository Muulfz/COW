using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Onset.Runtime
{
    internal class Wrapper
    {
        internal const int ApiVersion = 1;

        internal static Server Server { get; private set; }


        /*[DllImport("Onset.Runtime.dll", EntryPoint = "execute_lua")] //maybe change name?
        internal static extern string ExecuteLua(string name, bool simple, string data);*/

        [DllImport("Onset.Runtime.dll", EntryPoint = "print_to_console")]
        internal static extern void PrintToConsole(string message);

        internal static string ExecuteEvent(string name, string data)
        {
            return "";
        }

        internal static void Load()
        {
            PrintToConsole("Loading COW...");
            Server = new Server();
            Server.Start();
            PrintToConsole("COW loaded successfully!");
        }

        internal static void Unload()
        {
            Server.Stop();
        }
    }
}
