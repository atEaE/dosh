using CommandLine;
using Dosh.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Configuration command
    /// </summary>
    [Verb("config")]
    public class Config : CommandBase
    {
        [Option('i', "init")]
        public string Init { get; set; }

        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected override void OnExecute()
        {
            show();
        }

        /// <summary>
        /// Displays the application's settings.
        /// </summary>
        private void show()
        {
            var path = @"C:\Workspace\10_個人開発\OSS\dosh\Dosh\Dosh\bin\Debug\Initializer";
            var plugins = PluginLoader.LoadInitializerPlugins(path);

            try
            {
                var result = plugins[Init];
                result.Initialize();
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine("Not exists dll.");
            }


            //var appSettingsKeys = ConfigurationManager.AppSettings.AllKeys;
            //foreach (var key in appSettingsKeys)
            //{
            //    var value = ConfigurationManager.AppSettings[key];
            //    Console.WriteLine($"{key} = {value}");
            //}
        }
    }
}
