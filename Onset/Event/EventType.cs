﻿namespace Onset.Event
{
    /// <summary>
    /// All event types which can be listened to.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> quits the server.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerQuit = 0,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> writes something in the chat.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> message)
        /// </summary> 
        PlayerChat = 1,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> starts executing a command.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> command, <see cref="bool"/> exists)
        /// </summary>
        PlayerChatCommand = 2,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> joins the server.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerJoin = 3,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> pickups a <see cref="Onset.Entities.IPickup"/>.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IPickup"/> pickup)
        /// </summary>
        PlayerPickupHit = 4,
        /// <summary>
        /// Called when the package was started.<br/>
        /// ()
        /// </summary>
        PackageStart = 6,
        /// <summary>
        /// Called when the package is being stopped.<br/>
        /// ()
        /// </summary>
        PackageStop = 7,
        /// <summary>
        /// Called on execution of the main thread.<br/>
        /// (<see cref="float"/> deltaSeconds)
        /// </summary>
        GameTick = 8,
        /// <summary>
        /// Called when a client tries to connect to the server.<br/>
        /// (<see cref="string"/> ip, <see cref="int"/> port)<br/>
        /// <returns>Returning false results in denying the connection request and kicking the client</returns>
        /// </summary>
        ClientConnectionRequest = 9,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> reached its target.<br/>
        /// (<see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCReachTarget = 10,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> is damaged.<br/>
        /// (<see cref="Onset.Entities.INPC"/> npc, <see cref="Onset.Enums.DamageType"/> damageType, <see cref="float"/> amount)
        /// </summary>
        NPCDamage = 11,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> is spawned.<br/>
        /// (<see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCSpawn = 12,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> dies.<br/>
        /// (<see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCDeath = 13,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> is streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCStreamIn = 14,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.INPC"/> is no longer streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCStreamOut = 15,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> enters a <see cref="Onset.Entities.IVehicle"/>.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IVehicle"/> vehicle, <see cref="int"/> seat)
        /// </summary>
        PlayerEnterVehicle = 16,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> leaves a <see cref="Onset.Entities.IVehicle"/>.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IVehicle"/> vehicle, <see cref="int"/> seat)
        /// </summary>
        PlayerLeaveVehicle = 17,
        /// <summary>
        /// Called when a <see cref="Onset.Enums.PlayerState"/> changes for a <see cref="Onset.Entities.IPlayer"/>.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Enums.PlayerState"/> newState, <see cref="Onset.Enums.PlayerState"/> oldState)
        /// </summary>
        PlayerStateChange = 18,
        /// <summary>
        /// Called when a vehicle respawns.<br/>
        /// (<see cref="Onset.Entities.IVehicle"/> vehicle)
        /// </summary>
        VehicleRespawn = 19,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IVehicle"/> is streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IVehicle"/> vehicle)
        /// </summary>
        VehicleStreamIn = 20,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IVehicle"/> is no longer streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IVehicle"/> vehicle)
        /// </summary>
        VehicleStreamOut = 21,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> has been authorized by the server and is called after <see cref="ClientConnectionRequest"/> and before <see cref="PlayerJoin"/><br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerServerAuth = 22,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> has been authorized by Steam. After this call <see cref="Onset.Entities.IPlayer.SteamID"/> is available.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerSteamAuth = 23,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> finished downloading.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> fileName, <see cref="string"/> checksum)
        /// </summary>
        PlayerDownloadFile = 24,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> is streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IPlayer"/> other)
        /// </summary>
        PlayerStreamIn = 25,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> is no longer streamed for a player.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IPlayer"/> other)
        /// </summary>
        PlayerStreamOut = 26,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> is respawning.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player)
        /// </summary>
        PlayerSpawn = 27,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> dies.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IPlayer"/> killer)
        /// </summary>
        PlayerDeath = 28,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> has shot their weapon.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Enums.Weapon"/> weapon, <see cref="Onset.Enums.HitType"/> hitType, <see cref="Onset.Entities.IEntity"/> target, <see cref="Onset.Dimension.Vector"/> hitPos, <see cref="Onset.Dimension.Vector"/> startPos, <see cref="Onset.Dimension.Vector"/> impactPos)<br/>
        /// <returns>Returning false results in preventing the hit from further processing</returns>
        /// </summary>
        PlayerWeaponShot = 29,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> is damaged.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Enums.DamageType"/> damageType, <see cref="float"/> amount)
        /// </summary>
        PlayerDamage = 30,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> interacts with a door.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="Onset.Entities.IDoor"/> door, <see cref="bool"/> isBeingOpened)
        /// </summary>
        PlayerInteractDoor = 31,
        /// <summary>
        /// Called when a <see cref="Onset.Entities.IPlayer"/> wants to execute a command, but it fails with a <see cref="Onset.CommandError"/>.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> commandName, <see cref="string"/>[] args, <see cref="Onset.CommandError"/> error)<br/>
        /// </summary>
        PlayerCommandFailed = 32,
        /// <summary>
        /// Called before the command is being processed.<br/>
        /// (<see cref="Onset.Entities.IPlayer"/> player, <see cref="string"/> commandName, <see cref="string"/>[] args)
        /// /// <returns>Returning false results in preventing the further processing</returns>
        /// </summary>
        PlayerPreCommand = 32,
    }
}
