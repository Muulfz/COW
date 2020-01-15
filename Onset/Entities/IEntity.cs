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

        /// <summary>
        /// Sets the given value with the given property key.
        /// </summary>
        /// <param name="key">The key of the property value to be set</param>
        /// <param name="value">The value of the property</param>
        /// <param name="sync">Whether the property should be network synced or not</param>
        void SetProperty(string key, object value, bool sync = false);

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <typeparam name="T">The type of the returning value</typeparam>
        /// <param name="key">The key of the property</param>
        /// <param name="default">The default value which will be returned if none value is set</param>
        /// <returns>The property value to the given key or default</returns>
        T GetProperty<T>(string key, T @default = default);

        /// <summary>
        /// Checks if the given property key is set.
        /// </summary>
        /// <typeparam name="T">The type of the needed value</typeparam>
        /// <param name="key">The property key to be checked</param>
        /// <returns>True if the key has a value</returns>
        bool HasProperty<T>(string key);
    }
}
