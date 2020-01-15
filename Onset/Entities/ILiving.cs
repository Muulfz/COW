using Onset.Enums;

namespace Onset.Entities
{
    /// <summary>
    /// Represents a living entity which offers some functionality with interacting.
    /// Although it says living entity, the only living entity inheriting this is the player.
    /// The NPC is also a child of living but does not live itself.
    /// </summary>
    public interface ILiving : IEntity
    {
        /// <summary>
        /// The state of the ragdoll of the living entity.
        /// </summary>
        bool IsRagdoll { set; }

        /// <summary>
        /// The health of the living entity.
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// The heading (yaw) of the given living entity.
        /// </summary>
        float Heading { get; set; }

        /// <summary>
        /// Plays an animation to the given living entity.
        /// </summary>
        /// <param name="animation">The animation to be played</param>
        void Animate(Animation animation);
    }
}
