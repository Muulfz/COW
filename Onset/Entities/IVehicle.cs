using Onset.Dimension;
using Onset.Enums;
using Onset.Utils;

namespace Onset.Entities
{
    /// <summary>
    /// Represents a vehicle in the world of onset.
    /// </summary>
    public interface IVehicle : ILifeless
    {
        /// <summary>
        /// Teh license plate of the vehicle.
        /// </summary>
        string LicensePlate { get; set; }

        /// <summary>
        /// The model of the vehicle.
        /// </summary>
        VehicleModel Model { get; }

        /// <summary>
        /// The health of the vehicle.
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// The heading (yaw) of the given vehicle.
        /// </summary>
        float Heading { get; set; }

        /// <summary>
        /// The velocity of the vehicle.
        /// </summary>
        float Velocity { get; }

        /// <summary>
        /// The driver of the vehicle.
        /// Null when no one is driving.
        /// </summary>
        IPlayer Driver { get; }

        /// <summary>
        /// The number of seats in the vehicle.
        /// </summary>
        int Seats { get; }

        /// <summary>
        /// The color of the vehicle.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// The current gear of the vehicle.
        /// </summary>
        int Gear { get; }

        /// <summary>
        /// The open ratio of the hood in degrees. 0 is closed.
        /// </summary>
        float Hood { get; set; }

        /// <summary>
        /// The open ratio of the trunk in degrees. 0 is closed.
        /// </summary>
        float Trunk { get; set; }

        /// <summary>
        /// The engine state of the vehicle.
        /// </summary>
        bool EngineState { get; set; }

        /// <summary>
        /// The light state of the vehicle.
        /// </summary>
        bool LightState { get; set; }

        /// <summary>
        /// The color of the light of the vehicle.
        /// </summary>
        Color LightColor { get; }

        /// <summary>
        /// Checks if the vehicle is streamed to the given player.
        /// </summary>
        /// <param name="player">The player to be checked</param>
        /// <returns>True if the vehicle is streamed to the given player</returns>
        bool IsStreamedFor(IPlayer player);

        /// <summary>
        /// Sets the pre-vehicle respawn parameters.
        /// </summary>
        /// <param name="enable">When true the respawning for this vehicle is enabled</param>
        /// <param name="time">The respawn time in milliseconds</param>
        /// <param name="repairOnRespawn">When true the vehicle gets repaired on respawn</param>
        void SetRespawnParams(bool enable, long time = 0, bool repairOnRespawn = true);

        /// <summary>
        /// Sets the linear velocity of the vehicle.
        /// </summary>
        /// <param name="vector">The vector describing the axis velocity</param>
        /// <param name="reset">If the vehicle should be reset</param>
        void SetLinearVelocity(Vector vector, bool reset = false);

        /// <summary>
        /// Sets the angular velocity of the vehicle.
        /// </summary>
        /// <param name="vector">The vector describing the axis velocity</param>
        /// <param name="reset">If the vehicle should be reset</param>
        void SetAngularVelocity(Vector vector, bool reset = false);

        /// <summary>
        /// Attaches nitro to the vehicle.
        /// </summary>
        void AttachNitro();

        /// <summary>
        /// Returns the passenger on the given seat.
        /// </summary>
        /// <param name="seat">The seat to be selected</param>
        /// <returns>The player on the seat or null if none is sitting on the seat</returns>
        IPlayer GetPassenger(int seat);

        /// <summary>
        /// Sets the vehicle nitro enabled state.
        /// </summary>
        /// <param name="enabled">The nitro state</param>
        void SetNitro(bool enabled);

        /// <summary>
        /// Detaches nitro from the vehicle.
        /// </summary>
        void DetachNitro();

        /// <summary>
        /// Returns the damage on the given vehicle part.
        /// </summary>
        /// <param name="index">The vehicle part index between 1 and 8</param>
        /// <returns>The damage between 0 (no damage) and 1 (full damage)</returns>
        float GetDamage(int index);

        /// <summary>
        /// Damages the given vehicle part with the given damage value.
        /// </summary>
        /// <param name="index">The vehicle part index between 1 and 8</param>
        /// <param name="damage">The damage between 0 (no damage) and 1 (full damage)</param>
        void Damage(int index, float damage);
    }
}
