using Onset.Dimension;
using Onset.Entities;
using Onset.Enums;
using Onset.Utils;

namespace Onset.Runtime.Entities
{
    internal class Vehicle : Lifeless, IVehicle
    {
        public string LicensePlate
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleLicensePlate", new {entity = ID}).Value<string>("v");
            set => Wrapper.ExecuteLua("COW_GetVehicleLicensePlate", new { entity = ID, v = value });
        }

        public VehicleModel Model => (VehicleModel)Wrapper.ExecuteLua("COW_GetVehicleModel", new { entity = ID }).Value<int>("v");

        public float Health
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleHealth", new { entity = ID }).Value<float>("v");
            set => Wrapper.ExecuteLua("COW_GetVehicleHealth", new { entity = ID, v = value });
        }

        public float Heading
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleHeading", new { entity = ID }).Value<float>("v");
            set => Wrapper.ExecuteLua("COW_GetVehicleHeading", new { entity = ID, v = value });
        }

        public float Velocity => Wrapper.ExecuteLua("COW_GetVehicleVelocity", new { entity = ID }).Value<float>("v");

        public IPlayer Driver
        {
            get
            {
                long id = Wrapper.ExecuteLua("COW_GetVehicleDriver", new {entity = ID}).Value<long>("v");
                if (id <= 0) return null;
                return Wrapper.Server.PlayerPool.GetEntity(id);
            }
        }

        public int Seats => Wrapper.ExecuteLua("COW_GetVehicleNumberOfSeats", new { entity = ID }).Value<int>("v");

        public Color Color
        {
            get => new Color(Wrapper.ExecuteLua("COW_GetVehicleColor", new { entity = ID }).Value<string>("v"));
            set => Wrapper.ExecuteLua("COW_GetVehicleColor", new { entity = ID, v = value.ToHtmlHex() });
        }

        public int Gear => Wrapper.ExecuteLua("COW_GetVehicleGear", new {entity = ID}).Value<int>("v");

        public float Hood
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleHood", new { entity = ID }).Value<float>("v");
            set => Wrapper.ExecuteLua("COW_GetVehicleHood", new { entity = ID, v = value });
        }

        public float Trunk
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleTrunk", new { entity = ID }).Value<float>("v");
            set => Wrapper.ExecuteLua("COW_GetVehicleTrunk", new { entity = ID, v = value });
        }

        public bool EngineState
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleEngineState", new { entity = ID }).Value<bool>("v");
            set => Wrapper.ExecuteLua(value ? "COW_StartVehicleEngine" : "COW_StopVehicleEngine", new { entity = ID });
        }

        public bool LightState 
        {
            get => Wrapper.ExecuteLua("COW_GetVehicleLightState", new { entity = ID }).Value<bool>("v");
            set => Wrapper.ExecuteLua("COW_SetVehicleLightEnabled", new { entity = ID, v = value });
        }

        public Color LightColor => new Color(Wrapper.ExecuteLua("COW_GetVehicleLightColor", new { entity = ID }).Value<string>("v"));

        public Vehicle(long id) : base(id, "Vehicle")
        {
        }

        public bool IsStreamedFor(IPlayer player)
        {
            return Wrapper.ExecuteLua("COW_IsVehicleStreamedIn", new { entity = ID, player = player.ID })
                .Value<bool>("state");
        }

        public void SetRespawnParams(bool enable, long time = 0, bool repairOnRespawn = true)
        {
            Wrapper.ExecuteLua("COW_SetVehicleRespawnParams",
                new {entity = ID, state = enable, time, repair = repairOnRespawn});
        }

        public void SetLinearVelocity(Vector vector, bool reset = false)
        {
            Wrapper.ExecuteLua("COW_SetVehicleLinearVelocity", new {entity = ID, x = vector.X, y = vector.Y, z = vector.Z, reset});
        }

        public void SetAngularVelocity(Vector vector, bool reset = false)
        {
            Wrapper.ExecuteLua("COW_SetVehicleAngularVelocity", new { entity = ID, x = vector.X, y = vector.Y, z = vector.Z, reset });
        }

        public void AttachNitro()
        {
            SetNitro(true);
        }

        public IPlayer GetPassenger(int seat)
        {
            long id = Wrapper.ExecuteLua("COW_GetVehiclePassenger", new {entity = ID, seat}).Value<long>("val");
            if (id <= 0) return null;
            return Wrapper.Server.PlayerPool.GetEntity(id);
        }

        public void SetNitro(bool enabled)
        {
            Wrapper.ExecuteLua("COW_AttachVehicleNitro", new { entity = ID, state = enabled });
        }

        public void DetachNitro()
        {
            SetNitro(false);
        }

        public float GetDamage(int index)
        {
            return Wrapper.ExecuteLua("COW_GetVehicleDamage", new {entity = ID, index}).Value<float>("dmg");
        }

        public void Damage(int index, float damage)
        {
            Wrapper.ExecuteLua("COW_SetVehicleDamage", new {entity = ID, index, damage});
        }
    }
}
