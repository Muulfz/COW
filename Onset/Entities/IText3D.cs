using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Enums;

namespace Onset.Entities
{
    /// <summary>
    /// Represents a 3D text in the world of Onset.
    /// </summary>
    public interface IText3D : ILifeless
    {
        /// <summary>
        /// The text content of the 3D text.
        /// </summary>
        string Text { set; }

        /// <summary>
        /// Sets the visibility of the 3D text for the given players to the given visible state.
        /// </summary>
        /// <param name="visible">The visible state to be set</param>
        /// <param name="players">The players for which the setting will be applied</param>
        void SetVisibilityFor(bool visible, params IPlayer[] players);

        /// <summary>
        /// Sets the 3D text visibility for the given players to true.
        /// <see cref="SetVisibilityFor(bool,Onset.Entities.IPlayer[])"/>
        /// </summary>
        /// <param name="players">The players for which the setting will be applied</param>
        void SetVisibleFor(params IPlayer[] players);

        /// <summary>
        /// Attaches the 3D text to the given entity.
        /// </summary>
        /// <param name="type">The attach type</param>
        /// <param name="entityTo">The entity the 3D text gets attached to</param>
        /// <param name="position">The vector the 3D text gets positioned by</param>
        /// <param name="r">The r vector for rx, ry, rz</param>
        /// <param name="socketName">The name of the attaching socket</param>
        /// <returns>True on success</returns>
        bool AttachTo(AttachType type, IEntity entityTo, Vector position, Vector r = null, string socketName = "");
    }
}
