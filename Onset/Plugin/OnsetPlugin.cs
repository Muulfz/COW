namespace Onset.Plugin
{
    /// <summary>
    /// This defines the main entry point for the plugins. It offers the basic functionality and
    /// access point to the onset api.
    /// </summary>
    public abstract class OnsetPlugin
    {
        /// <summary>
        /// The server is the main entry point to the server api of onset.
        /// </summary>
        public IServer Server { get; internal set; }

        /// <summary>
        /// The meta of this plugin.
        /// </summary>
        public Meta Meta { get; internal set; }

        /// <summary>
        /// The logger of this plugin.
        /// </summary>
        public ILogger Logger { get; internal set; }

        /// <summary>
        /// The current state of this plugin.
        /// </summary>
        public PluginState State { get; internal set; } = PluginState.Unknown;

        /// <summary>
        /// Loads the plugin and starts it after loading is complete.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Unloads the plugin and disables it.
        /// </summary>
        public abstract void Unload();
    }
}
