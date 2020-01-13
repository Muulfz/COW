using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;
using Onset.Runtime.Entities;

namespace Onset.Runtime.Pools
{
    internal class EntityPool<T> where T : IEntity
    {
        internal List<T> Entities { get; }

        private readonly Func<long, T> _entityFactory; 

        internal EntityPool(Func<long, T> entityFactory)
        {
            _entityFactory = entityFactory;
            Entities = new List<T>();
        }

        internal T GetEntity(long id)
        {
            lock (Entities)
            {
                T entity = SearchEntity(id);
                if (entity != null) return entity;
                entity = _entityFactory.Invoke(id);
                Entities.Add(entity);
                return entity;
            }
        }

        internal void RemoveEntity(T entity)
        {
            lock (Entities)
            {
                if (Entities.Contains(entity))
                {
                    Entities.Remove(entity);
                }
            }
        }

        private T SearchEntity(long id)
        {
            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                T entity = Entities[i];
                if (entity.ID == id)
                {
                    return entity;
                }
            }

            return default;
        }
    }
}
