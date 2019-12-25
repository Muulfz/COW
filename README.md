# Project COW [![Version](https://img.shields.io/badge/version-1.0%3A0-green)](https://github.com/DasDarki/COW)

COW stands for **C**Sharp **O**nset **W**rapper and is - as the name says - a Wrapper for the LUA API for Onset Servers.

## Features
  - Create Plugins in .NET Core 3.0 with out limitations
  - Cross-Platform compatibility (Windows and Linux are tested)
  - Use the full spectrum of the Server-Side LUA API in C#
  - Load other C# Libraries by simply let it load by the COW Runtime
  - Interact with others C# Plugins or either other LUA Scripts
  - A powerfull API which offers the newest standards
  - No need to interact with C++ or the Runtime itself


## Requirements
  - .NET Core =3.0
  - Onset Server
  - One package slot available (keep in mind, there is a package limitation, and COW is an own package)
  - For developers: C# knowledge and NUGET

## Installation
*coming soon*

## Team
Name  | Role | Link
------------- | -------------| -------------
Das Darki  | Project Lead & Dev Lead | [Github](https://github.com/DasDarki/)

### License [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
COW is licensed under the **MIT** License.

## ToDo
- Client-Side API (*maybe not possible*, workaround?)
- Async Support
- Dependency Loading / Prior Loading

## Documentaion
*coming soon*

### Getting Started
This is a guide that shows you how to start writing with the COW plugin. We will go through all the steps step by step and explain the absolute basic structure. There will not be a complete tutorial for the complete API, because it is much too big. For more information after this guide, we recommend the documentation of the API itself (see HERE)

#### Step 1: Setup IDE
To use the API you need a .NET Core 3.0 project. We won't go through the steps how to create a project. After the project is created, go to NUGET and search for *TODO PLEASE ENTER NUGET HERE* and install the latest version.

#### Step 2: Create Main Class
Now we can create the main class. Every plugin must have a main class. The plugin manager of COW will than use this class as entry point and will manage your plugin from that point. There are some important which are required, so that there are no problemes with the plugin.
First the basic structure:
```csharp
using Onset.Plugin;

namespace TestPlugin
{
    [Meta("test-plugin", 1, "1.0")]
    public class PluginMain : OnsetPlugin
    {
        public override void Load()
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}
```
First of all, the using-declaration. You need to use the namespace **Onset.Plugin**. The main namespace is **Onset**.
Secondly the class must extend OnsetPlugin. Its an abstract class and will force you to override Load and Unload. Load gets called, when your plugin gets loaded and Unload, when it gets unloaded.
Third and also really import, the class must marked with a Meta. The meta defines some important information about your Plugin. The first argument of the meta is the ID of your plugin. Every plugin must have a unique ID. The second is the API version of the current COW API. If the API of one plugin is lower than the current running API, the plugin won't load. Third, the plugin version. You wil need a plugin version, but it won't be a problem, if you fill an empty string.

#### To be continued...
