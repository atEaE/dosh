using CommandLine;
using System;

namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Initialize command
    /// </summary>
    [Verb("init")]
    public class Init : CommandBase
    {
        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected override void OnExecute()
        {
            Console.WriteLine("Init command Execute");           
        }
    }
}
