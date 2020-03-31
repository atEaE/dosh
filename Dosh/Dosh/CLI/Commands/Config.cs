using CommandLine;
using System;
using System.Configuration;

namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Configuration command
    /// </summary>
    [Verb("config")]
    public class Config : CommandBase
    {
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
            var appSettingsKeys = ConfigurationManager.AppSettings.AllKeys;
            foreach (var key in appSettingsKeys)
            {
                var value = ConfigurationManager.AppSettings[key];
                Console.WriteLine($"{key} = {value}");
            }
        }
    }
}
