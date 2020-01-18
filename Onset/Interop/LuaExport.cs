using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Interop
{
    /// <summary>
    /// Marks a method as lua exported function for interoping with C# from LUA.
    /// Valid types are <see cref="string"/>, any kind of number, <see cref="bool"/> and any kind of <see cref="Onset.Entities.IEntity"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LuaExport : Attribute
    {
        /// <summary>
        /// The name of the function, if not set, it takes the method name as function name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The default constructor which can take a name.
        /// </summary>
        /// <param name="name">The name of the function, leaving null results in method name taking</param>
        public LuaExport(string name = null)
        {
            Name = name;
        }
    }
}
