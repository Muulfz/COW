using System;
using System.Net;
using Onset.Dimension;
using Onset.Enums;

namespace Onset.Entities
{
    /// <summary>
    /// This interface represents the players on the server and gives control over them.
    /// </summary>
    public interface IPlayer : ILiving
    {
        /// <summary>
        /// The name of this player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The 64-bit steam id of this player.
        /// Is 0, if the player was not yet authenticated.
        /// </summary>
        long SteamID { get; }

        /// <summary>
        /// The players head size between 0.0 and 3.0.
        /// </summary>
        float HeadSize { get; set; }

        /// <summary>
        /// The network stats from this player.
        /// </summary>
        NetworkStats NetworkStats { get; }

        /// <summary>
        /// The activation state of the player's voice chat.
        /// </summary>
        bool IsVoiceEnabled { get; set; }

        /// <summary>
        /// The state if the player is currently taking or not.
        /// </summary>
        bool IsTalking { get; }

        /// <summary>
        /// The dimension which is needed for the voice chat calculation.
        /// </summary>
        IDimension VoiceDimension { set; }

        /// <summary>
        /// The current state of the player.
        /// </summary>
        PlayerState State { get; }

        /// <summary>
        /// The current <see cref="MoveMode"/> of the player.
        /// </summary>
        MoveMode Movement { get; }

        /// <summary>
        /// The current movement speed of the player.
        /// </summary>
        float Speed { get; }

        /// <summary>
        /// The state if the player is currently aiming.
        /// </summary>
        bool IsAiming { get; }

        /// <summary>
        /// The state if the player is currently reloading.
        /// </summary>
        bool IsReloading { get; }

        /// <summary>
        /// The vehicle the player is currently in, or null if in none.
        /// </summary>
        IVehicle Vehicle { get; }

        /// <summary>
        /// The current vehicle seat the player sits on.
        /// </summary>
        int VehicleSeat { get; }

        /// <summary>
        /// The game version of the player.
        /// </summary>
        string GameVersion { get; }

        /// <summary>
        /// The global unique identifier of the player.
        /// </summary>
        Guid GUID { get; }

        /// <summary>
        /// The locale of the player.
        /// </summary>
        string Locale { get; }

        /// <summary>
        /// The network ping of the player.
        /// </summary>
        int Ping { get; }

        /// <summary>
        /// The hostname of the player.
        /// </summary>
        IPAddress Hostname { get; }

        /// <summary>
        /// The delay of the respawning of the player in milliseconds.
        /// </summary>
        long RespawnTime { get; set; }

        /// <summary>
        /// The armor of the player.
        /// </summary>
        int Armor { get; set; }

        /// <summary>
        /// The state if the player is dead or alive.
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        /// The currently equipped weapon of the player.
        /// </summary>
        Weapon EquippedWeapon { get; }

        /// <summary>
        /// The currently selected weapon equip slot of the player.
        /// </summary>
        int SelectedSlot { get; set; }

        /// <summary>
        /// Disables the spectate mode.
        /// </summary>
        void Unspectate();

        /// <summary>
        /// Enables the spectate mode.
        /// </summary>
        void Spectate();

        /// <summary>
        /// Sets the player's spectate mode enabled or disabled.
        /// </summary>
        /// <param name="enable">True means the spectate mode is enabled</param>
        void SetSpectate(bool enable);

        /// <summary>
        /// Kicks the player with the given reason
        /// </summary>
        /// <param name="reason">The reason of the kick</param>
        void Kick(string reason = "");
        
        /// <summary>
        /// Sets the given weapon stat for the given weapon to the given value.
        /// </summary>
        /// <param name="weapon">The weapon id to be set</param>
        /// <param name="stat">The weapon stat to be set</param>
        /// <param name="value">The new value of the stat</param>
        void SetWeaponStat(Weapon weapon, WeaponStat stat, float value);

        /// <summary>
        /// Sets the given equip slot to the given weapon by the given parameters.
        /// </summary>
        /// <param name="weapon">The weapon model to be set</param>
        /// <param name="ammo">The ammo of the weapon</param>
        /// <param name="slot">The equip slot to be set (1 - 3)</param>
        /// <param name="equip">True if the weapon should be equipped after giving</param>
        /// <param name="loaded">True if the weapon should be loaded with a magazine after giving</param>
        void SetWeapon(Weapon weapon, int ammo, int slot = 1, bool equip = true, bool loaded = true);

        /// <summary>
        /// Returns the weapon currently equipped at the given equip slot.
        /// </summary>
        /// <param name="slot">The equip slot to check</param>
        /// <param name="ammo">The ammo of the weapon</param>
        /// <returns>The weapon model</returns>
        Weapon GetWeapon(int slot, out int ammo);

        /// <summary>
        /// Sets the player into the given vehicle on the given seat.
        /// </summary>
        /// <param name="vehicle">The vehicle in which the player will be sit in</param>
        /// <param name="seat">The new seat of the player</param>
        void SetIntoVehicle(IVehicle vehicle, int seat = 1);

        /// <summary>
        /// Removes the player from the vehicle he is sitting in.
        /// </summary>
        void RemoveFromVehicle();

        /// <summary>
        /// Triggers a remote event on the client of this current player.
        /// </summary>
        /// <param name="name">The name of the remote event to be triggered</param>
        /// <param name="args">The arguments which will be sent to the client side</param>
        void CallRemote(string name, params object[] args);

        /// <summary>
        /// Sends this player a message to the chat.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        void SendMessage(string message);

        /// <summary>
        /// Attaches this player a parachute.
        /// </summary>
        void AttachParachute();

        /// <summary>
        /// Detaches this player the parachute.
        /// </summary>
        void DetachParachute();

        /// <summary>
        /// Sets the pickup visibility to the given state.
        /// </summary>
        /// <param name="pickup">The pickup to which the visibility belongs to</param>
        /// <param name="visible">The visible which will be set</param>
        void SetPickupVisibility(IPickup pickup, bool visible = true);

        /// <summary>
        /// Sets the 3D text visibility to the given state.
        /// </summary>
        /// <param name="text">The 3D text to which the visibility belongs to</param>
        /// <param name="visible">The visible which will be set</param>
        void SetText3DVisibility(IText3D text, bool visible = true);

        /// <summary>
        /// Sets the spawn location of the player.
        /// </summary>
        /// <param name="position">The x/y/z coordinates how the player spawns</param>
        /// <param name="heading">The heading (yaw) how the player spawns</param>
        void SetSpawnLocation(Vector position, float heading = 0);


    }
}
