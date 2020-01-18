using System;
using Onset.Dimension;
using Onset.Entities;
using Onset.Plugin;
using System.Collections.Generic;

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
        /// The global server dimension with the default ID of 0.
        /// </summary>
        IDimension Global { get; }

        /// <summary>
        /// The list containing all players on the current running server.
        /// </summary>
        List<IPlayer> AllPlayers { get; }

        /// <summary>
        /// The list containing all doors on the current running server.
        /// </summary>
        List<IDoor> AllDoors { get; }

        /// <summary>
        /// The list containing all npcs on the current running server.
        /// </summary>
        List<INPC> AllNPCs { get; }

        /// <summary>
        /// The list containing all pickups on the current running server.
        /// </summary>
        List<IPickup> AllPickups { get; }

        /// <summary>
        /// The list containing all 3D texts on the current running server.
        /// </summary>
        List<IText3D> AllText3Ds { get; }

        /// <summary>
        /// The list containing all objects on the current running server.
        /// </summary>
        List<IObject> AllObjects { get; }

        /// <summary>
        /// The list containing all vehicles on the current running server.
        /// </summary>
        List<IVehicle> AllVehicles { get; }

        /// <summary>
        /// The list containing all packages running on the current running server.
        /// </summary>
        List<string> AllPackages { get; }

        /// <summary>
        /// The tick rate of the main thread. The rate is variable and depends on the load of the server. For an empty server this is between 500Hz and 1000Hz. For 300 concurrent players the tick rate should be above 150Hz.
        /// </summary>
        float TickRate { get; }

        /// <summary>
        /// The display name of this server.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The player slots count.
        /// </summary>
        int MaxPlayers { get; }

        /// <summary>
        /// Shutdowns the server.
        /// <param name="reason">The reason with which the server shutdowns</param>
        /// </summary>
        void Exit(string reason = "");

        /// <summary>
        /// Starts the package by the given name.
        /// </summary>
        /// <param name="packageName">The package name of the wanted package</param>
        void StartPackage(string packageName);

        /// <summary>
        /// Stops the package by the given name.
        /// </summary>
        /// <param name="packageName">The package name of the wanted package</param>
        void StopPackage(string packageName);

        /// <summary>
        /// Checks if the given package is started or not.
        /// </summary>
        /// <param name="packageName">The package name of the wanted package</param>
        /// <returns>True if the given package by the given name is started</returns>
        bool IsPackageStarted(string packageName);

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

        /// <summary>
        /// Returns the dimension object of the given dimension id.
        /// </summary>
        /// <param name="id">The id of the wanted dimension</param>
        /// <returns>The dimension object of the given id</returns>
        IDimension GetDimension(uint id);

        /// <summary>
        /// Broadcasts the given message to all players.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        void Broadcast(string message);

        /// <summary>
        /// Broadcasts the given message to all players in the range around the given position.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="position">The position defining the center</param>
        /// <param name="range">The radius in which the message gets sent</param>
        void Broadcast(string message, Vector position, float range);

        /// <summary>
        /// Multicasts the given message to all given players.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="players">All players which should receive the message</param>
        void Multicast(string message, IEnumerable<IPlayer> players);

        /// <summary>
        /// Multicasts the given message to all given players.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="players">All players which should receive the message</param>
        void Multicast(string message, params IPlayer[] players);

        /// <summary>
        /// Executes the given action in the main thread.
        /// </summary>
        /// <param name="task">The action which represents the wanted task</param>
        void ExecuteTask(Action task);
    }
}
