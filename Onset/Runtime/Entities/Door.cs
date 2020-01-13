using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Door : Lifeless, IDoor
    {
        public ushort Model => Wrapper.ExecuteLua("COW_GetDoorModel", new { entity = ID }).Value<ushort>("model");

        public bool IsOpen
        {
            get => Wrapper.ExecuteLua("COW_IsDoorOpen", new {entity = ID}).Value<bool>("state");
            set => Wrapper.ExecuteLua("COW_SetDoorOpen", new {entity = ID, state = value});
        }

        internal Door(long id) : base(id, "Door")
        {
        }
    }
}
