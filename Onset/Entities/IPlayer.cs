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

        /// <summary>
        /// Triggers a remote event on the client of this current player.
        /// </summary>
        /// <param name="name">The name of the remote event to be triggered</param>
        /// <param name="args">The arguments which will be sent to the client side</param>
        void CallRemote(string name, params object[] args);

        /// <summary>
        /// Sends this player a message to the chat.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        void SendMessage(string message);
    }
}
