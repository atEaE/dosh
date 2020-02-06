using CommandLine;
using Dosh.CLI.Commands;

namespace Dosh.CLI
{
    /// <summary>
    /// Entry class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Init, Run>(args);
            result.WithParsed<Init>(cmd => cmd.Execute())
                  .WithParsed<Run>(cmd => cmd.Execute());
        }
    }
}
