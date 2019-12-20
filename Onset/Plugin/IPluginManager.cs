using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Plugin
{
    /// <summary>
    /// The plugin manager loads and manages all plugins
    /// and its dependencies.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// All plugins which are loaded and managed by this plugin manager instance.
        /// </summary>
        List<OnsetPlugin> Plugins { get; }

        /// <summary>
        /// Returns the plugin by the given id of the wanted plugin.
        /// </summary>
        /// <param name="id">The id of the wanted plugin</param>
        /// <returns>The plugin instance or null</returns>
        OnsetPlugin GetPlugin(string id);

        /// <summary>
        /// Returns the plugin by a given predicate selector.
        /// </summary>
        /// <param name="select">The predicate selector</param>
        /// <returns>The plugin or null</returns>
        OnsetPlugin GetPlugin(Predicate<OnsetPlugin> select);

        /// <summary>
        /// Starts and registers the given plugin instance. Please use this method with attention
        /// otherwise unexpected behavior could happen.
        /// </summary>
        /// <param name="plugin">The plugin to be enabled</param>
        void StartPlugin(OnsetPlugin plugin);

        /// <summary>
        /// Stops the given plugin and unregisters it from this plugin manager.
        /// </summary>
        /// <param name="plugin"></param>
        void StopPlugin(OnsetPlugin plugin);
    }
}
