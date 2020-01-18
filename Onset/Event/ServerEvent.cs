using System;

namespace Onset.Event
{
    /// <summary>
    /// Methods which have this attribute are marked as server events
    /// handling these events. Every handling method can only handle one event.
    /// Some events are cancellable which means they can be cancelled at any time.
    /// When events are cancelled the process connected with this event will be immediately cancelled.
    /// To cancel an event, the must just have to return false (as a boolean return method). But the method don't need
    /// a return type, but when, than the only type allowed is boolean, indicating the cancel.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ServerEvent : Attribute
    {
        /// <summary>
        /// The event type handled by this marked method.
        /// </summary>
        public EventType Type { get; }

        public ServerEvent(EventType type)
        {
            Type = type;
        }
    }
}
