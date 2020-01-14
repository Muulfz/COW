using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Entities;
using Onset.Runtime.Garbage;

namespace Onset.Runtime.Entities
{
    internal abstract class Entity : IEntity
    {
        public long ID { get; }

        public IDimension Dimension 
        { 
            get => Wrapper.Server.GetDimension(Wrapper.ExecuteLua("COW_Get" + EntityName + "Dimension", new { entity = ID }).Value<uint>("dim"));
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Dimension", new { dim = value.ID, entity = ID });
        }

        public Vector Position
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Position", new { entity = ID }).ExtractPosition();
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Position", new { entity = ID, x = value.X, y = value.Y, z = value.Z });
        }

        public bool IsValid => CheckValidation();

        protected readonly string EntityName;

        protected Entity(long id, string entityName)
        {
            ID = id;
            EntityName = entityName;
        }

        public void SetDimension(uint id)
        {
            Dimension = Wrapper.Server.GetDimension(id);
        }

        public virtual void SetProperty(string key, object value, bool sync = false)
        {
            Wrapper.ExecuteLua("COW_Set" + EntityName + "Property", new { key, value, sync, entity = ID });
        }

        public virtual T GetProperty<T>(string key, T @default = default)
        {
            T value = Wrapper.ExecuteLua("COW_Get" + EntityName + "Property", new {key, entity = ID}).Value<T>("value");
            if (value.Equals(default)) return @default;
            return value;
        }

        public virtual bool HasProperty<T>(string key)
        {
            return !GetProperty<T>(key).Equals(default);
        }

        protected bool CheckValidation()
        {
            bool state = Wrapper.ExecuteLua("COW_Get" + EntityName + "Validation", new { entity = ID }).Value<bool>("state");
            if (!state)
            {
                Cleaner.Kick(this);
            }
            return state;
        }
    }
}
