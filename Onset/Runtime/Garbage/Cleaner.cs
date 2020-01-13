using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Runtime.Garbage
{
    internal class Cleaner
    {
        private static readonly Dictionary<Type, object> Handlers = new Dictionary<Type, object>();

        internal static void Kick<T>(T garbage)
        {
            Type type = typeof(T);
            if (Handlers.ContainsKey(type))
            {
                ((IGarbageHandler<T>) Handlers[type]).Handle(garbage);
            }
        }

        internal static void RegisterHandler<T>(IGarbageHandler<T> handler)
        {
            Handlers.Add(typeof(T), handler);
        }
    }
}
