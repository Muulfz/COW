using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;

namespace Onset.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// The onset server id of this entity.
        /// </summary>
        long ID { get; }

        /// <summary>
        /// The onset world dimension of this entity.
        /// </summary>
        IDimension Dimension { get; set; }

        /// <summary>
        /// The position of the entity.
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// Checks if the entity is valid or not.
        /// If the entity is not valid the COW cleaner will be kicked off
        /// causing the entities deletion out of the COW system.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Sets this entities dimension via the given dimension id.
        /// </summary>
        /// <param name="id">The id of the wanted dimension</param>
        void SetDimension(uint id);
    }
}
