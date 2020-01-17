using Onset.Entities;
using System.Collections.Generic;
using Onset.Enums;

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
        /// A list with all pickups currently in this dimension.
        /// </summary>
        List<IPickup> Pickups { get; }

        /// <summary>
        /// A list with all objects currently in this dimension.
        /// </summary>
        List<IObject> Objects { get; }

        /// <summary>
        /// A list with all 3D texts currently in this dimension.
        /// </summary>
        List<IText3D> Text3Ds { get; }

        /// <summary>
        /// A list with all vehicles currently in this dimension.
        /// </summary>
        List<IVehicle> Vehicles { get; }

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
        /// <param name="model">The module of the pickup (<see cref="https://dev.playonset.com/wiki/Objects">Object List</see>)</param>
        /// <param name="position">The position at which the pickup will be spawned</param>
        /// <returns>The created pickup</returns>
        IPickup CreatePickup(ulong model, Vector position);

        /// <summary>
        /// Creates an <see cref="IObject"/> in this dimension by the given parameters.
        /// </summary>
        /// <param name="model">The module of the object (<see cref="https://dev.playonset.com/wiki/Objects">Object List</see>)</param>
        /// <param name="position">The position at which the object will be spawned</param>
        /// <param name="rotation">The rotation of the object. Default: 0</param>
        /// <param name="scale">The scale of the object. Default: 1</param>
        /// <returns>The created pickup</returns>
        IObject CreateObject(ulong model, Vector position, Vector rotation = null, Vector scale = null);

        /// <summary>
        /// Creates a <see cref="IText3D"/> in this dimension by the given parameters.
        /// </summary>
        /// <param name="text">The text to be shown</param>
        /// <param name="size">The size of the text</param>
        /// <param name="position">The position of the text</param>
        /// <param name="r">The rotation of the text. Default: 0</param>
        /// <returns>The created 3D text</returns>
        IText3D CreateText3D(string text, float size, Vector position, Vector r = null);

        /// <summary>
        /// Creates a <see cref="IVehicle"/> in this dimension by the given parameters.
        /// </summary>
        /// <param name="model">The vehicle model (<see cref="VehicleModel"/>)</param>
        /// <param name="position">The position the vehicle gets spawned</param>
        /// <param name="heading">The yaw of the vehicle. Default: 0</param>
        /// <returns>The created vehicle</returns>
        IVehicle CreateVehicle(VehicleModel model, Vector position, float heading = 0);
    }
}
