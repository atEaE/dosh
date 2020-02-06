using CommandLine;
using System;

namespace Dosh.CLI.Commands
{
    /// <summary>
    ///  Test start command
    /// </summary>
    [Verb("run")]
    public class Run : CommandBase
    {
        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected override void OnExecute()
        {
            Console.WriteLine("Run command Execute");
        }
    }
}
