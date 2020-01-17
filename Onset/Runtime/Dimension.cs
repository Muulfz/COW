using Onset.Dimension;
using Onset.Entities;
using Onset.Helper;
using System.Collections.Generic;

namespace Onset.Runtime
{
    internal class Dimension : IDimension
    {
        public uint ID { get; }

        public List<IPlayer> Players => Wrapper.Server.AllPlayers.SelectAll(player => player.Dimension.ID == ID);

        public List<INPC> NPCs => Wrapper.Server.AllNPCs.SelectAll(player => player.Dimension.ID == ID);

        public List<IDoor> Doors => Wrapper.Server.AllDoors.SelectAll(player => player.Dimension.ID == ID);

        public List<IPickup> Pickups => Wrapper.Server.AllPickups.SelectAll(player => player.Dimension.ID == ID);

        public List<IObject> Objects => Wrapper.Server.AllObjects.SelectAll(player => player.Dimension.ID == ID);

        public List<IText3D> Text3Ds => Wrapper.Server.AllText3Ds.SelectAll(player => player.Dimension.ID == ID);

        internal Dimension(uint id)
        {
            ID = id;
        }

        public bool CreateExplosion(uint id, Vector position, bool hasSound = true, double camShakeRadius = 0, double radialForce = 0)
        {
            return Wrapper.ExecuteLua("COW_CreateExplosion",
                new { id, dim = ID, x = position.X, y = position.Y, z = position.Z, hasSound, camShakeRadius, radialForce }).Value<bool>("success");
        }

        public IDoor CreateDoor(ushort model, Vector position, double yaw, bool interactable = true)
        {
            IDoor door = Wrapper.Server.DoorPool.GetEntity(Wrapper.ExecuteLua("COW_CreateDoor",
                new { model, x = position.X, y = position.Y, z = position.Z, yaw, interactable }).Value<long>("door"));
            door.SetDimension(ID);
            return door;
        }

        public IPickup CreatePickup(ulong model, Vector position)
        {
            IPickup pickup = Wrapper.Server.PickupPool.GetEntity(Wrapper.ExecuteLua("COW_CreatePickup",
                new { model, x = position.X, y = position.Y, z = position.Z }).Value<long>("pickup"));
            pickup.SetDimension(ID);
            return pickup;
        }

        public IObject CreateObject(ulong model, Vector position, Vector rotation = null, Vector scale = null)
        {
            IObject obj = Wrapper.Server.ObjectPool.GetEntity(Wrapper.ExecuteLua("COW_CreateObject",
                new { model, x = position.X, y = position.Y, z = position.Z, rx = position.X, ry = position.Y, rz = position.Z, 
                    sx = position.X, sy = position.Y, sz = position.Z }).Value<long>("obje"));
            obj.SetDimension(ID);
            return obj;
        }

        public IText3D CreateText3D(string text, float size, Vector position, Vector r = null)
        {
            r = r ?? Vector.Empty;
            IText3D text3D = Wrapper.Server.Text3DPool.GetEntity(Wrapper.ExecuteLua("COW_CreateText3D", 
                new
                {
                    text, size, x = position.X, y = position.Y, z = position.Z, rx = r.X, ry = r.Y, rz = r.Z
                }).Value<long>("text"));
            text3D.SetDimension(ID);
            return text3D;
        }
    }
}
