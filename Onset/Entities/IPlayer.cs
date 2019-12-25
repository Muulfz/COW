using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Entities
{
    /// <summary>
    /// This interface represents the players on the server and gives control over them.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The onset server id of this player.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The name of this player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The 64-bit steam id of this player.
        /// Is 0, if the player was not yet authenticated.
        /// </summary>
        long SteamID { get; }
    }
}
