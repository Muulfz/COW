using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Enums;

namespace Onset.Entities
{
    /// <summary>
    /// Represents an object in the world of Onset.
    /// </summary>
    public interface IObject : ILifeless
    {
        /// <summary>
        /// The model of the object.
        /// </summary>
        ulong Model { get; }

        /// <summary>
        /// The rotation of the object.
        /// </summary>
        Vector Rotation { get; set; }

        /// <summary>
        /// The scale of the object.
        /// </summary>
        Vector Scale { get; set; }

        /// <summary>
        /// Whether the object is moving or not.
        /// </summary>
        bool IsMoving { get; }

        /// <summary>
        /// The rotation axis of the object.
        /// </summary>
        Vector RotationAxis { set; }

        /// <summary>
        /// Checks if the object is streamed to the given player.
        /// </summary>
        /// <param name="player">The player to be checked</param>
        /// <returns>True if the object is streamed to the given player</returns>
        bool IsStreamedFor(IPlayer player);

        /// <summary>
        /// Sets the given distance as streaming distance for this object.
        /// </summary>
        /// <param name="dist">The distance to be set</param>
        /// <returns>True on success</returns>
        bool SetStreamDistance(double dist);

        /// <summary>
        /// Returns the <see cref="AttachInfo"/> of the attachment of this object.
        /// </summary>
        /// <returns>The attach info</returns>
        AttachInfo GetAttachmentInfo();

        /// <summary>
        /// Attaches the object to the given entity.
        /// </summary>
        /// <param name="entityTo">The entity the 3D text gets attached to. The only entities allowed are defined by the <see cref="AttachType"/></param>
        /// <param name="position">The vector the 3D text gets positioned by</param>
        /// <param name="r">The rotation of the attachment</param>
        /// <param name="socketName">The name of the attaching socket</param>
        /// <returns>True on success</returns>
        bool AttachTo(IEntity entityTo, Vector position, Vector r = null, string socketName = "");

        /// <summary>
        /// Detaches this object if it got attached before.
        /// </summary>
        void Detach();

        /// <summary>
        /// Moves the object to the given position with the given speed.
        /// </summary>
        /// <param name="position">The position the object walks to</param>
        /// <param name="speed">The speed of the walking</param>
        void WalkTo(Vector position, float speed = 160);

        /// <summary>
        /// Stops the object from walking.
        /// </summary>
        void StopWalking();
    }
}
