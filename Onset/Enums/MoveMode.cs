namespace Onset.Enums
{
    /// <summary>
    /// Defines the player's move mode and state.
    /// </summary>
    public enum MoveMode
    {
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is standing still.
        /// </summary>
        StandingStill,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> aims with e.g. a gun while walking.
        /// </summary>
        AimWalking,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is just walking.
        /// </summary>
        Walking,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is running.
        /// </summary>
        Running,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is crouching
        /// </summary>
        Crouched,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is falling.
        /// </summary>
        Falling,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is sky diving.
        /// </summary>
        Skydiving,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> has its parachute open.
        /// </summary>
        Parachuting,
        /// <summary>
        /// A <see cref="Onset.Entities.IPlayer"/> is swimming.
        /// </summary>
        Swimming
    }
}
