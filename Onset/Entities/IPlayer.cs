using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Onset.Enums;

namespace Onset.Entities
{
    /// <summary>
    /// This interface represents the players on the server and gives control over them.
    /// </summary>
    public interface IPlayer : IEntity
    {

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
        /// The players head size between 0.0 and 3.0.
        /// </summary>
        float HeadSize { get; set; }

        /// <summary>
        /// The network stats from this player.
        /// </summary>
        NetworkStats NetworkStats { get; }

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

        /// <summary>
        /// Attaches this player a parachute.
        /// </summary>
        void AttachParachute();

        /// <summary>
        /// Detaches this player the parachute.
        /// </summary>
        void DetachParachute();

        /// <summary>
        /// Plays an animation to the given player.
        /// </summary>
        /// <param name="animation">The animation to be played</param>
        void Animate(Animation animation);
    }
}
