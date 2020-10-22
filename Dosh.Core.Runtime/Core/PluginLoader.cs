using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dosh.Core
{
    /// <summary>
    /// Plugin loader
    /// </summary>
    public static class PluginLoader
    {
        public static IDictionary<string, Provider.Initializer.IInitializer> LoadPlugins(string path)
        {
            var plugins = new Dictionary<string, Provider.Initializer.IInitializer>();
            if (Directory.Exists(path))
            {
                var dlls = Directory.GetFiles(path, "*.dll").Select(p => Path.GetFullPath(p)).ToArray();
                var pluginTypes = new Dictionary<string, Type>();
                foreach(var dll in dlls)
                {
                    var assm = Assembly.LoadFrom(dll);
                    var name = assm.GetName().Name;
                    if (assm != null)
                    {
                        var types = assm.GetTypes();
                        foreach(var type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(typeof(Provider.Initializer.IInitializer).FullName) != null)
                                {
                                    pluginTypes.Add(name, type);
                                }
                            }
                        }
                    }
                }

                foreach(var pType in pluginTypes)
                {
                    var plugin = (Provider.Initializer.IInitializer)Activator.CreateInstance(pType.Value);
                    plugins.Add(pType.Key, plugin);
                }
                return plugins;
            }
            return plugins;
        }
    }
}
