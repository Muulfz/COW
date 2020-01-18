using System;
using System.Collections.Generic;
using System.Text;
using Onset.Runtime;

namespace Onset.Interop
{
    /// <summary>
    /// The main interoping entry point. From here all interops will be managed.
    /// </summary>
    public class LuaInterop
    {
        private static readonly Registry<LuaExport> ExportedFunctions = CreateRegistry();

        private static Registry<LuaExport> CreateRegistry()
        {
            Registry<LuaExport> registry = new Registry<LuaExport>();
            registry.ItemRegistered += item =>
            {
                item.Data.Name = item.Data.Name ?? item.Invoker.Name;
            };
            return registry;
        }

        internal static void CallInterop(string json)
        {
            ReturnData data = new ReturnData(json);
            object[] args = data.Value<object[]>("args");
            string name = data.Value<string>("name");
            ExportedFunctions.Execute(item => item.Data.Name == name, args);
        }

        /// <summary>
        /// Creates an object from the given type and searches through this object for <see cref="Onset.Interop.LuaExport"/> and registers them in the registry.
        /// </summary>
        /// <typeparam name="T">The type which gets created. Make sure the type has a default constructor!</typeparam>
        public static void RegisterExports<T>()
        {
            ExportedFunctions.Register<T>();
        }

        /// <summary>
        /// Searches through the given object for <see cref="Onset.Interop.LuaExport"/> and registers them in the registry.
        /// </summary>
        /// <param name="obj">The object to be searched through</param>
        public static void RegisterExports(object obj)
        {
            ExportedFunctions.Register(obj);
        }

        /// <summary>
        /// Executes a lua function marked as lua import.
        /// </summary>
        /// <param name="function">The function name to be called</param>
        /// <param name="args">The arguments to send with. Valid types are <see cref="string"/>, any kind of number, <see cref="bool"/> and any kind of <see cref="Onset.Entities.IEntity"/></param>
        public static void Execute(string function, params object[] args)
        {
            Wrapper.ExecuteLua("COW_ExecuteLuaImport", new {name = function, args});
        }
    }
}
