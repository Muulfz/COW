using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Event
{
    /// <summary>
    /// All event types which can be listened to.
    /// </summary>
    public enum EventType
    {
        //Never change the order of the enum!
        /// <summary>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerQuit,
        /// <summary>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> message)
        /// </summary> 
        PlayerChat,
        /// <summary>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> command, <see cref="bool"/> exists)
        /// </summary>
        PlayerChatCommand,
        /// <summary>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerJoin
    }
}
