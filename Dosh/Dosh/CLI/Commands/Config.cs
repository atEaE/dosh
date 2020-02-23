using CommandLine;
using System;

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
            Console.WriteLine("Config command is not yet implemented.");
        }
    }
}
