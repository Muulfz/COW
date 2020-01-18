using System;
using System.Collections.Generic;
using System.Text;

namespace Onset
{
    /// <summary>
    /// Methods marked with this command attribute are marked as command handlers.
    /// Each handling method must have a <see cref="Onset.Entities.IPlayer"/> as first argument
    /// followed by the arguments of the command. Arguments can be optional.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Command : Attribute
    {
        /// <summary>
        /// The name of the command defining the chat root.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The main constructor requiring the command's name.
        /// </summary>
        /// <param name="name">The name of the command</param>
        public Command(string name)
        {
            Name = name;
        }
    }
}
