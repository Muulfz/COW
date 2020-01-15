using Onset.Entities;
using System.Collections.Generic;

namespace Onset.Dimension
{
    /// <summary>
    /// Represents every dimension in onset from here you can interact with them
    /// or the entities in this dimension. 
    /// </summary>
    public interface IDimension
    {
        /// <summary>
        /// The id of this dimension.
        /// </summary>
        uint ID { get; }

        /// <summary>
        /// A list with all players currently in this dimension.
        /// </summary>
        List<IPlayer> Players { get; }

        /// <summary>
        /// A list with all npcs currently in this dimension.
        /// </summary>
        List<INPC> NPCs { get; }

        /// <summary>
        /// A list with all doors currently in this dimension.
        /// </summary>
        List<IDoor> Doors { get; }

        /// <summary>
        /// A list with all picks currently in this dimension.
        /// </summary>
        List<IPickup> Pickups { get; }

        /// <summary>
        /// Creates an explosion in this dimension.
        /// </summary>
        /// <param name="id">The number which identifies the explosion</param>
        /// <param name="position">The position at which the explosion will be spawned</param>
        /// <param name="hasSound">Whether the explosion has a sound or not</param>
        /// <param name="camShakeRadius">The radius in which the cam will shake</param>
        /// <param name="radialForce">The radial force of the explosion</param>
        /// <returns>True on success</returns>
        bool CreateExplosion(uint id, Vector position, bool hasSound = true, double camShakeRadius = 0, double radialForce = 0);

        /// <summary>
        /// Creates a <see cref="IDoor"/> in this dimension by the given parameters.
        /// </summary>
        /// <param name="model">The model of the Door (<see cref="https://dev.playonset.com/wiki/Doors">Model List</see>)</param>
        /// <param name="position">The position at which the door will be spawned</param>
        /// <param name="yaw">The yaw heading of the door</param>
        /// <param name="interactable">Whether a player can interact with door or not</param>
        /// <returns>The created door</returns>
        IDoor CreateDoor(ushort model, Vector position, double yaw, bool interactable = true);

        /// <summary>
        /// Creates a <see cref="IPickup"/> in this dimension by the given parameters.
        /// </summary>
        /// <param name="model">The module of the Pickup (<see cref="https://dev.playonset.com/wiki/Objects">Object List</see>)</param>
        /// <param name="position">The position at which the pickup will be spawned</param>
        /// <returns>The created pickup</returns>
        IPickup CreatePickup(ulong model, Vector position);
    }
}
