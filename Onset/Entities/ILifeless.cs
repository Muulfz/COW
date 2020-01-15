namespace Onset.Entities
{

    /// <summary>
    /// Represents a non living entity.
    /// </summary>
    public interface ILifeless : IEntity
    {
        /// <summary>
        /// Destroys this entity.
        /// </summary>
        void Destroy();
    }
}
