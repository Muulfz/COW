using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;
using Onset.Enums;

namespace Onset.Runtime.Entities
{
    internal abstract class Living : Entity, ILiving
    {
        public bool IsRagdoll
        {
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Ragdoll", new {entity = ID, state = value});
        }

        public float Health
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Health").Value<float>("health"); 
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Health", new {entity = ID, health = value});
        }

        public float Heading
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Heading").Value<float>("heading");
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Heading", new { entity = ID, heading = value });
        }

        protected Living(long id, string entityName) : base(id, entityName)
        {
        }

        public void Animate(Animation animation)
        {
            Wrapper.ExecuteLua("COW_Set" + EntityName + "Animation", new { entity = ID, anim = animation.GetName() });
        }
    }
}
