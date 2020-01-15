using Onset.Dimension;

namespace Onset.Entities
{
    /// <summary>
    /// Represents the main base class for all entities. Everything which can be spawned, or which spawns
    /// automatically and stays as long as it gets destroys, is an entity.
    /// </summary>
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
        /// <exception cref="System.NotImplementedException">When using Position on <see cref="IText3D"/></exception>
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

        /// <summary>
        /// Sets the given value with the given property key.
        /// </summary>
        /// <param name="key">The key of the property value to be set</param>
        /// <param name="value">The value of the property</param>
        /// <param name="sync">Whether the property should be network synced or not</param>
        /// <exception cref="System.NotImplementedException">When using SetProperty on <see cref="IDoor"/></exception>
        void SetProperty(string key, object value, bool sync = false);

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <typeparam name="T">The type of the returning value</typeparam>
        /// <param name="key">The key of the property</param>
        /// <param name="default">The default value which will be returned if none value is set</param>
        /// <returns>The property value to the given key or default</returns>
        /// <exception cref="System.NotImplementedException">When using GetProperty on <see cref="IDoor"/></exception>
        T GetProperty<T>(string key, T @default = default);

        /// <summary>
        /// Checks if the given property key is set.
        /// </summary>
        /// <typeparam name="T">The type of the needed value</typeparam>
        /// <param name="key">The property key to be checked</param>
        /// <returns>True if the key has a value</returns>
        /// <exception cref="System.NotImplementedException">When using HasProperty on <see cref="IDoor"/></exception>
        bool HasProperty<T>(string key);
    }
}
