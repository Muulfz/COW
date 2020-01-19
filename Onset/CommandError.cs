using System;
using System.Collections.Generic;
using System.Text;

namespace Onset
{
    /// <summary>
    /// An enum representing the errors which can happen when failing a command execution.
    /// </summary>
    public enum CommandError
    {
        /// <summary>
        /// Occurs when the requested command is none registered command.
        /// </summary>
        NotExisting, 
        /// <summary>
        /// Occurs when the player did not enter the required number of arguments.
        /// </summary>
        TooFewArguments
    }
}
