using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Entities
{
    /// <summary>
    /// Represents a door in the world of onset. Players can interact with doors by pressing 'E'.
    /// Some of them are physics doors meaning they are pushed open by running against them.
    /// </summary>
    public interface IDoor : ILifeless
    {

        /// <summary>
        /// The model of the door (<see cref="https://dev.playonset.com/wiki/Doors">Model List</see>).
        /// </summary>
        ushort Model { get; }

        /// <summary>
        /// The state of the door. Whether the door is open or not.
        /// </summary>
        bool IsOpen { get; set; }
    }
}
