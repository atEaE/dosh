using Dosh.Core.Provider.Initializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dosh.Core.Plugin
{
    /// <summary>
    /// Plugin loader
    /// </summary>
    public static class PluginLoader
    {
        /// <summary>
        /// Load the Initializer plugins.
        /// </summary>
        /// <param name="directoryPath">dll folderpath</param>
        /// <returns><see cref="IInitializer"/> plugins</returns>
        public static IDictionary<string, IInitializer> LoadInitializerPlugins(string directoryPath)
        {
            return LoadPlugins<IInitializer>(directoryPath); 
        }

        /// <summary>
        /// Reads the .dll from the given folder.
        /// </summary>
        /// <typeparam name="T">interface only</typeparam>
        /// <param name="directoryPath">dll folderpath</param>
        /// <returns>Plugins</returns>
        public static IDictionary<string, T> LoadPlugins<T>(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }

            var plugins = new Dictionary<string, T>();
            var dllPaths = Directory.GetFiles(directoryPath, "*.dll").Select(p => Path.GetFullPath(p));
            foreach(var dll in dllPaths)
            {
                var assm = Assembly.LoadFrom(dll);
                if (assm == null)
                {
                    throw new DllNotFoundException(dll);
                }
                var name = assm.GetName().Name;
                var types = assm.GetTypes();
                foreach(var type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
  
                    if (type.GetInterface(typeof(T).FullName) != null)
                    {
                        var plugin = (T)Activator.CreateInstance(type);
                        
                        if (plugins.ContainsKey(name))
                        {
                            // If the key already exists, an error occurs
                            // If you are creating a plugin, there must be only one class that extends the Dosh.Core.Provider interface.
                            throw new BadImageFormatException($"{name}.dll is in violation of the plugin creation rules.");
                        }
                        plugins.Add(name, plugin);
                    }
                }
            }
            return plugins;
        }
    }
}
