using System;

namespace Onset.Event
{
    /// <summary>
    /// Methods which have this attribute are marked as handlers for remote events, triggered by a client.
    /// The first argument of the method must be the <see cref="Entities.IPlayer"/> of the triggering client.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RemoteEvent : Attribute
    {
        /// <summary>
        /// The key defines at which point the handler needs to trigger.
        /// </summary>
        public string Key { get; }

        public RemoteEvent(string key)
        {
            Key = key;
        }
    }
}
