using Onset.Dimension;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class NPC : Living, INPC
    {
        public NPC(long id) : base(id, "NPC")
        {
        }

        public void WalkTo(Vector position, float speed = 160)
        {
            Wrapper.ExecuteLua("COW_SetNPCTargetLocation", new { entity = ID, x = position.X, y = position.Y, z = position.Z, speed });
        }

        public void Follow(IVehicle vehicle, float speed = 160)
        {
            Wrapper.ExecuteLua("COW_SetNPCFollowVehicle", new { entity = ID, vehicle = vehicle.ID, speed });
        }

        public void Follow(IPlayer player, float speed = 160)
        {
            Wrapper.ExecuteLua("COW_SetNPCFollowPlayer", new { entity = ID, player = player.ID, speed });
        }

        public bool IsStreamedFor(IPlayer player)
        {
            return Wrapper.ExecuteLua("COW_IsNPCStreamedIn", new { entity = ID, player = player.ID })
                .Value<bool>("state");
        }

        public void Destroy()
        {
            Wrapper.ExecuteLua("COW_Destroy" + EntityName, new { entity = ID });
            CheckValidation();
        }
    }
}
