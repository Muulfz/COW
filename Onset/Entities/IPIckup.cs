using Onset.Dimension;
using Onset.Event;

namespace Onset.Entities
{
    /// <summary>
    /// Represents an object which can be picked up. When a vehicle or a player picks the object up, the
    /// belonging event <see cref="EventType.PlayerPickupHit"/> (player) or <see cref="EventType.VehiclePickupHit"/> (vehicle)
    /// gets called.
    /// </summary>
    public interface IPickup : ILifeless
    {
        /// <summary>
        /// The scale of the pickup.
        /// </summary>
        Vector Scale { get; set; }

        /// <summary>
        /// Sets the visibility of the pickup for the given players to the given visible state.
        /// </summary>
        /// <param name="visible">The visible state to be set</param>
        /// <param name="players">The players for which the setting will be applied</param>
        void SetVisibilityFor(bool visible, params IPlayer[] players);

        /// <summary>
        /// Sets the pickup visibility for the given players to true.
        /// <see cref="SetVisibilityFor(bool,Onset.Entities.IPlayer[])"/>
        /// </summary>
        /// <param name="players">The players for which the setting will be applied</param>
        void SetVisibleFor(params IPlayer[] players);
    }
}
