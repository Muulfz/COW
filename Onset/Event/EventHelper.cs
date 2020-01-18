using System.Collections.Generic;

namespace Onset.Event
{
    /// <summary>
    /// The EventHelper has some utility functions for the event section of the onset server.
    /// </summary>
    public static class EventHelper
    {
        private static readonly List<EventType> PlayerEvents = new List<EventType>
        {
            EventType.PlayerChat, EventType.PlayerChatCommand, EventType.PlayerJoin, EventType.PlayerQuit, EventType.PlayerPickupHit,
            EventType.NPCStreamIn, EventType.NPCStreamOut, EventType.PlayerEnterVehicle, EventType.PlayerLeaveVehicle, EventType.PlayerStateChange,
            EventType.VehicleStreamIn, EventType.VehicleStreamOut, EventType.PlayerDamage, EventType.PlayerDeath, EventType.PlayerInteractDoor,
            EventType.PlayerStreamIn, EventType.PlayerStreamOut, EventType.PlayerServerAuth, EventType.PlayerSteamAuth, EventType.PlayerDownloadFile,
            EventType.PlayerWeaponShot, EventType.PlayerSpawn
        };

        /// <summary>
        /// Returns a boolean whether the given event is a player event - so needs a player as the first argument - or not.
        /// </summary>
        /// <param name="type">The event type</param>
        /// <returns>True if it is a player event</returns>
        public static bool IsPlayerEvent(this EventType type)
        {
            return PlayerEvents.Contains(type);
        }
    }
}
