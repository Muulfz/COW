using Onset.Entities;
using System;

namespace Onset.Runtime.Entities
{
    internal class Door : Lifeless, IDoor
    {
        public ushort Model => Wrapper.ExecuteLua("COW_GetDoorModel", new { entity = ID }).Value<ushort>("model");

        public bool IsOpen
        {
            get => Wrapper.ExecuteLua("COW_IsDoorOpen", new { entity = ID }).Value<bool>("state");
            set => Wrapper.ExecuteLua("COW_SetDoorOpen", new { entity = ID, state = value });
        }

        internal Door(long id) : base(id, "Door")
        {
        }

        public override void SetProperty(string key, object value, bool sync = false)
        {
            throw new NotImplementedException("SetProperty is illegal on Doors!");
        }

        public override T GetProperty<T>(string key, T @default = default)
        {
            throw new NotImplementedException("GetProperty is illegal on Doors!");
        }

        public override bool HasProperty<T>(string key)
        {
            throw new NotImplementedException("HasProperty is illegal on Doors!");
        }
    }
}
