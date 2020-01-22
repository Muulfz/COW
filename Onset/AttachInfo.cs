using Onset.Entities;
using Onset.Enums;

namespace Onset
{
    /// <summary>
    /// Containing information about an attachment.
    /// </summary>
    public struct AttachInfo
    {
        /// <summary>
        /// The type of the attachment.
        /// </summary>
        public AttachType Type { get; }

        /// <summary>
        /// The entity on which the attachment got attached to.
        /// </summary>
        public IEntity Entity { get; }

        internal AttachInfo(AttachType type, IEntity entity)
        {
            Type = type;
            Entity = entity;
        }
    }
}
