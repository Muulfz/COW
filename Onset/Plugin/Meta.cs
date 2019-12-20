using System;
using System.Collections.Generic;
using System.Text;
using Onset.Runtime;

namespace Onset.Plugin
{
    /// <summary>
    /// The meta defines some needed information about the plugin.
    /// It also defines which class the main class is and where to start from.
    /// Consider reading every property for more information.
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Class)]
    public class Meta : Attribute
    {
        /// <summary>
        /// The id is represents the plugin. The id is required and must be unique.
        /// If two plugins have the some id, the first will be loaded and the second won't.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// The name of the plugins. This is just for representation and is not needed.
        /// If the name is not set, the id will be the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The author of the plugin. This property is also optional
        /// and can be empty.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The version of the plugin is needed. It can be used to define which version
        /// of an specified addon is needed so that the addon can work.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The description of the plugin is optional. It defines what the plugin
        /// is doing or what it should do.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The api version must be set. The version defines which api is needed minimum to run this plugin.
        /// The the current runtime has a lower api version than this plugin, the loader will tell the user to update
        /// the runtime and won't load the plugin to avoid errors.
        /// </summary>
        public int ApiVersion { get; set; }

        /// <summary>
        /// The dependencies of the plugin. IDs from other plugins can be entered which this plugin needs to work.
        /// If the plugin is loaded and a dependency not, the loader will wait until all its dependencies are loaded
        /// and than load this plugin.
        /// </summary>
        public string[] Dependencies { get; set; }

        /// <summary>
        /// This flag defines if the plugin is in debug mode which enables debug messages and some other debug functionality.
        /// The Onset Wrapper warns the developer, that plugins with debug modes enabled are active.
        /// </summary>
        public bool IsDebug { get; set; } = false;

        public Meta(string id, int apiVersion, string version, string name = null, string author = "", string description = "", params string[] dependencies)
        {
            ID = id;
            ApiVersion = apiVersion;
            Name = name ?? id;
            Version = version;
            Author = author;
            Dependencies = dependencies;
            Description = description;
        }
    }
}
