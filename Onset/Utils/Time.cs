using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Onset.Runtime;

namespace Onset.Utils
{
    /// <summary>
    /// Represents the time functions of onset.
    /// </summary>
    public class Time
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long _lastNanos;

        /// <summary>
        /// Starts a nano test.
        /// </summary>
        public static void StartTest()
        {
            _lastNanos = NanoTime();
        }

        /// <summary>
        /// Stops the nano test and returns the nanoseconds which have been passed
        /// since starting the test with <see cref="StartTest"/>
        /// </summary>
        /// <returns></returns>
        public static long StopTest()
        {
            return NanoTime() - _lastNanos;
        }

        private static long NanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        /// <summary>
        /// Returns the amount of time since the game started up.
        /// </summary>
        /// <returns>The time as float</returns>
        public static float GetTimeSeconds()
        {
            return Wrapper.ExecuteLua("COW_GetTimeSeconds").Value<float>("val");
        }

        /// <summary>
        /// Returns the delta seconds of the current game running.
        /// </summary>
        /// <returns>The delta as float</returns>
        public static float GetDeltaSeconds()
        {
            return Wrapper.ExecuteLua("COW_GetDeltaSeconds").Value<float>("val");
        }

        /// <summary>
        /// Returns the uptime of the actual server (not the Onset server) in milliseconds.
        /// </summary>
        /// <returns>The tick count as long</returns>
        public static long GetTickCount()
        {
            return Wrapper.ExecuteLua("COW_GetTickCount").Value<long>("val");
        }

        /// <summary>
        /// Returns the current time from January 1st 1970 to now in milliseconds.
        /// </summary>
        /// <returns>The milliseconds as long</returns>
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1St1970).TotalMilliseconds;
        }
    }
}
