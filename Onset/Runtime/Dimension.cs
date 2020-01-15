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
    }
}
