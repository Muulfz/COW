using System;
using System.Collections.Generic;
using System.Text;

namespace Onset
{
    public interface ILogger
    {
        /// <summary>
        /// Prints an info message to the console.
        /// </summary>
        /// <param name="message">The message to be printed</param>
        void Info(string message);

        /// <summary>
        /// Prints a warning message to the console.
        /// </summary>
        /// <param name="message">The message to be printed</param>
        void Warn(string message);

        /// <summary>
        /// Prints a success message to the console.
        /// </summary>
        /// <param name="message">The message to be printed</param>
        void Success(string message);

        /// <summary>
        /// Prints a debug message to the console.
        /// The debug message is only visible as long as the plugin is in debug mode (In the Meta attribute, set the Debug flag to true)
        /// </summary>
        /// <param name="message">The message to be printed</param>
        void Debug(string message);

        /// <summary>
        /// Prints a fatal message to the console.
        /// </summary>
        /// <param name="message">The message to be printed</param>
        void Fatal(string message);

        /// <summary>
        /// Prints an error message to the console.
        /// </summary>
        /// <param name="message">The message to be printed</param>
        /// <param name="exception">The exception to be appended</param>
        void Error(string message, Exception exception);
    }
}
