using System;
using System.Net;
using Onset.Dimension;
using Onset.Entities;
using Onset.Enums;

namespace Onset.Runtime.Entities
{
    internal class Player : Living, IPlayer
    {
        public string Name
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerName", new { player = ID }).Value<string>("playerName");
            set => Wrapper.ExecuteLua("COW_SetPlayerName", new { player = ID, playerName = value });
        }

        public long SteamID => Wrapper.ExecuteLua("COW_GetPlayerSteamID", new { player = ID }).Value<long>("steamID");

        public float HeadSize
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerHeadSize", new { player = ID }).Value<float>("size");
            set => Wrapper.ExecuteLua("COW_SetPlayerHeadSize", new { player = ID, size = value });
        }

        public NetworkStats NetworkStats => new NetworkStats(Wrapper.ExecuteLua("COW_GetPlayerNetworkStats", new { player = ID }));

        public bool IsVoiceEnabled { get; set; }

        public bool IsTalking => Wrapper.ExecuteLua("COW_IsPlayerTalking", new { entity = ID }).Value<bool>("v");

        public IDimension VoiceDimension
        {
            set => Wrapper.ExecuteLua("COW_SetPlayerVoiceDimension", new { v = value.ID, entity = ID });
        }

        public PlayerState State =>
            (PlayerState) Wrapper.ExecuteLua("COW_GetPlayerState", new {entity = ID}).Value<int>("v");

        public MoveMode Movement =>
            (MoveMode) Wrapper.ExecuteLua("COW_GetPlayerMovementMode", new { entity = ID }).Value<int>("v");

        public float Speed => Wrapper.ExecuteLua("COW_GetPlayerSpeed", new { entity = ID }).Value<float>("v");

        public bool IsAiming => Wrapper.ExecuteLua("COW_IsPlayerAiming", new { entity = ID }).Value<bool>("v");

        public bool IsReloading => Wrapper.ExecuteLua("COW_IsPlayerReloading", new { entity = ID }).Value<bool>("v");

        public IVehicle Vehicle =>
            Wrapper.Server.VehiclePool.GetEntity(Wrapper.ExecuteLua("COW_GetPlayerVehicle", new {entity = ID})
                .Value<long>("v"));

        public int VehicleSeat => Wrapper.ExecuteLua("COW_GetPlayerVehicleSeat", new {entity = ID})
            .Value<int>("v");

        public string GameVersion => Wrapper.ExecuteLua("COW_GetPlayerGameVersion", new {entity = ID})
            .Value<string>("v");

        public Guid GUID => Guid.Parse(Wrapper.ExecuteLua("COW_GetPlayerGUID", new { entity = ID })
            .Value<string>("v"));

        public string Locale => Wrapper.ExecuteLua("COW_GetPlayerLocale", new {entity = ID})
            .Value<string>("v");

        public int Ping => Wrapper.ExecuteLua("COW_GetPlayerPing", new {entity = ID})
            .Value<int>("v");

        public IPAddress Hostname => IPAddress.Parse(Wrapper.ExecuteLua("COW_GetPlayerIP", new {entity = ID})
            .Value<string>("v"));

        public long RespawnTime
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerRespawnTime", new { entity = ID }).Value<long>("v");
            set => Wrapper.ExecuteLua("COW_SetPlayerRespawnTime", new { entity = ID, v = value });
        }

        public int Armor
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerArmor", new { entity = ID }).Value<int>("v");
            set => Wrapper.ExecuteLua("COW_SetPlayerArmor", new { entity = ID, v = value });
        }

        public bool IsDead => Wrapper.ExecuteLua("COW_IsPlayerDead", new {entity = ID})
            .Value<bool>("v");

        public Weapon EquippedWeapon => (Weapon) Wrapper.ExecuteLua("COW_GetPlayerEquippedWeapon", new {entity = ID})
            .Value<int>("v");

        public int SelectedSlot
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerEquippedWeaponSlot", new { entity = ID }).Value<int>("v");
            set => Wrapper.ExecuteLua("COW_EquipPlayerWeaponSlot", new { entity = ID, v = value });
        }

        internal Player(long id) : base(id, "Player")
        {
        }

        public void CallRemote(string name, params object[] args)
        {
            Wrapper.ExecuteLua("COW_CallRemoteEvent", new { eventName = name, player = ID, args });
        }

        public void SendMessage(string message)
        {
            Wrapper.ExecuteLua("COW_AddPlayerChat", new { player = ID, message });
        }

        public void AttachParachute()
        {
            Wrapper.ExecuteLua("COW_AttachPlayerParachute", new { player = ID, enable = true });
        }

        public void DetachParachute()
        {
            Wrapper.ExecuteLua("COW_AttachPlayerParachute", new { player = ID, enable = false });
        }

        public void SetPickupVisibility(IPickup pickup, bool visible = true)
        {
            Wrapper.ExecuteLua("COW_SetPickupVisibility", new {player = ID, pickup = pickup.ID, visible});
        }

        public void SetText3DVisibility(IText3D text, bool visible = true)
        {
            Wrapper.ExecuteLua("COW_SetText3DVisibility", new { player = ID, text = text.ID, visible });
        }

        public void SetSpawnLocation(Vector position, float heading = 0)
        {
            Wrapper.ExecuteLua("COW_SetPlayerSpawnLocation", new { player = ID, x = position.X, y = position.Y, z = position.Z, heading });
        }

        public void Unspectate()
        {
            SetSpectate(false);
        }

        public void Spectate()
        {
            SetSpectate(true);
        }

        public void SetSpectate(bool enable)
        {
            Wrapper.ExecuteLua("COW_SetPlayerSpectate", new { player = ID, enable });
        }

        public void Kick(string reason = "")
        {
            Wrapper.ExecuteLua("COW_KickPlayer", new { player = ID, reason });
        }

        public void SetWeaponStat(Weapon weapon, WeaponStat stat, float value)
        {
            Wrapper.ExecuteLua("COW_SetPlayerWeaponStat", new { player = ID, weapon = (int) weapon, stat = (int) stat, value });
        }

        public void SetWeapon(Weapon weapon, int ammo, int slot = 1, bool equip = true, bool loaded = true)
        {
            Wrapper.ExecuteLua("COW_SetPlayerWeapon",
                new {player = ID, weapon = (int) weapon, ammo, equip, slot, loaded});
        }

        public Weapon GetWeapon(int slot, out int ammo)
        {
            ReturnData data = Wrapper.ExecuteLua("COW_GetPlayerWeapon", new {player = ID, slot});
            ammo = data.Value<int>("ammo");
            return (Weapon) data.Value<int>("weapon");
        }

        public void SetIntoVehicle(IVehicle vehicle, int seat = 0)
        {
            Wrapper.ExecuteLua("COW_SetPlayerIntoVehicle", new { player = ID, vehicle = vehicle.ID, seat });
        }

        public void RemoveFromVehicle()
        {
            Wrapper.ExecuteLua("COW_RemovePlayerFromVehicle", new { player = ID });
        }
    }
}
