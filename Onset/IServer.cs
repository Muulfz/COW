using System;
using System.Collections.Generic;
using System.Text;
using Onset.Plugin;

namespace Onset
{
    /// <summary>
    /// The server interface is the api for the server functionality of the wrapper.
    /// It represents the server of onset and all its functionality.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// The plugin manage of the current onset wrapper instance.
        /// </summary>
        IPluginManager PluginManager { get; }

        /// <summary>
        /// Returns the main logger of current COW instance.
        /// </summary>
        ILogger Logger { get; }
    }
}
