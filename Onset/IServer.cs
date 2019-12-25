using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;
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
        /// The main logger of current COW instance.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// The game version of the current running server.
        /// </summary>
        int GameVersion { get; }

        /// <summary>
        /// The list containing all players on the current running server.
        /// </summary>
        List<IPlayer> Players { get; }

        /// <summary>
        /// Searches through the given object for <see cref="Onset.Command"/> and registers them in the registry.
        /// </summary>
        /// <param name="obj">The object to be searched through</param>
        void RegisterCommands(object obj);

        /// <summary>
        /// Creates an object from the given type and searches through this object for <see cref="Onset.Command"/> and registers them in the registry.
        /// </summary>
        /// <typeparam name="T">The type which gets created. Make sure the type has a default constructor!</typeparam>
        void RegisterCommands<T>();

        /// <summary>
        /// Searches through the given object for <see cref="Onset.Event.ServerEvent"/> and registers them in the registry.
        /// </summary>
        /// <param name="obj">The object to be searched through</param>
        void RegisterEvents(object obj);

        /// <summary>
        /// Creates an object from the given type and searches through this object for <see cref="Onset.Event.ServerEvent"/> and registers them in the registry.
        /// </summary>
        /// <typeparam name="T">The type which gets created. Make sure the type has a default constructor!</typeparam>
        void RegisterEvents<T>();

        /// <summary>
        /// Searches through the given object for <see cref="Onset.Event.RemoteEvent"/> and registers them in the registry.
        /// </summary>
        /// <param name="obj">The object to be searched through</param>
        void RegisterClientEvents(object obj);

        /// <summary>
        /// Creates an object from the given type and searches through this object for <see cref="Onset.Event.RemoteEvent"/> and registers them in the registry.
        /// </summary>
        /// <typeparam name="T">The type which gets created. Make sure the type has a default constructor!</typeparam>
        void RegisterClientEvents<T>();
    }
}
