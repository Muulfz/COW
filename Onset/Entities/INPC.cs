using Onset.Dimension;

namespace Onset.Entities
{
    /// <summary>
    /// Represents a NPC in the world of Onset.
    /// </summary>
    /// ReSharper disable once InconsistentNaming
    public interface INPC : ILiving
    {
        /// <summary>
        /// Forces the NPC to walk to the given position with the given speed.
        /// </summary>
        /// <param name="position">The position the NPC should walk to</param>
        /// <param name="speed">The speed with which the NPC is walking (in cm)</param>
        void WalkTo(Vector position, float speed = 160);

        /// <summary>
        /// Forces the NPC to follow the given vehicle with the given speed.
        /// </summary>
        /// <param name="vehicle">The vehicle to be followed</param>
        /// <param name="speed">The speed with which the NPC is walking (in cm)</param>
        void Follow(IVehicle vehicle, float speed = 160);

        /// <summary>
        /// Forces the NPC to follow the given player with the given speed.
        /// </summary>
        /// <param name="player">The player to be followed</param>
        /// <param name="speed">The speed with which the NPC is walking (in cm)</param>
        void Follow(IPlayer player, float speed = 160);

        /// <summary>
        /// Checks if the NPC is streamed to the given player.
        /// </summary>
        /// <param name="player">The player to be checked</param>
        /// <returns>True if the NPC is streamed to the given player</returns>
        bool IsStreamedFor(IPlayer player);

        /// <summary>
        /// Destroys the NPC.
        /// </summary>
        void Destroy();
    }
}
