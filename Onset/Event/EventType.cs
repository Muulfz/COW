namespace Onset.Event
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
        /// Called when a <see cref="Onset.Entities.IVehicle"/> pickups a <see cref="Onset.Entities.IPickup"/>.<br/>
        /// (<see cref="Onset.Entities.IVehicle"/> vehicle, <see cref="Onset.Entities.IPickup"/> pickup)
        /// </summary>
        VehiclePickupHit = 5,
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
        /// Called when a <see cref="Onset.Entities.INPC"/> reached its target.
        /// (<see cref="Onset.Entities.INPC"/> npc)
        /// </summary>
        NPCReachTarget = 10,
        NPCDamage = 11,
        NPCSpawn = 12,
        NPCDeath = 13,
        NPCStreamIn = 14,
        NPCStreamOut = 15
    }
}
