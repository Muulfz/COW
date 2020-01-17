using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Entities;
using Onset.Enums;

namespace Onset.Runtime.Entities
{
    internal class Object : Lifeless, IObject
    {
        public ulong Model => Wrapper.ExecuteLua("COW_GetObjectModel", new {entity = ID}).Value<ulong>("model");

        public Vector Rotation
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Rotation", new { entity = ID }).ExtractPosition();
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Rotation", new { entity = ID, x = value.X, y = value.Y, z = value.Z });
        }

        public Vector Scale
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Scale", new { entity = ID }).ExtractPosition();
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Scale", new { entity = ID, x = value.X, y = value.Y, z = value.Z });
        }

        public bool IsMoving => Wrapper.ExecuteLua("COW_IsObjectMoving", new {entity = ID}).Value<bool>("state");

        public Vector RotationAxis
        {
            set =>
                Wrapper.ExecuteLua("COW_SetObjectRotationAxis",
                    new {entity = ID, x = value.X, y = value.Y, z = value.Z});
        }


        public Object(long id) : base(id, "Object")
        {
        }

        public bool IsStreamedFor(IPlayer player)
        {
            return Wrapper.ExecuteLua("COW_IsObjectStreamedIn", new { entity = ID, player = player.ID })
                .Value<bool>("state");
        }

        public bool SetStreamDistance(double dist)
        {
            return Wrapper.ExecuteLua("COW_SetObjectStreamDistance", new { entity = ID, dist})
                .Value<bool>("state");
        }

        public AttachInfo GetAttachmentInfo()
        {
            ReturnData data = Wrapper.ExecuteLua("COW_GetObjectAttachmentInfo", new {entity = ID});
            AttachType type = (AttachType) data.Value<int>();
            return new AttachInfo(type, Wrapper.GetEntityByAttach(data.Value<long>("attach"), type));
        }

        public bool AttachTo(IEntity entityTo, Vector position, Vector r = null, string socketName = "")
        {
            r = r ?? Vector.Empty;
            return Wrapper.ExecuteLua("COW_Set" + EntityName + "Attached",
                new
                {
                    entity = ID,
                    type = (int)Wrapper.GetAttachType(entityTo),
                    attach = entityTo.ID,
                    x = position.X,
                    y = position.Y,
                    z = position.Z,
                    rx = r.X,
                    ry = r.Y,
                    rz = r.Z,
                    socketName
                }).Value<bool>("state");
        }

        public void Detach()
        {
            Wrapper.ExecuteLua("COW_DetachObject", new {entity = ID});
        }

        public void WalkTo(Vector position, float speed = 160)
        {
            Wrapper.ExecuteLua("COW_DetachObject",
                new {entity = ID, speed, x = position.X, y = position.Y, z = position.Z});
        }

        public void StopWalking()
        {
            Wrapper.ExecuteLua("COW_StopObjectMove", new { entity = ID });
        }
    }
}
