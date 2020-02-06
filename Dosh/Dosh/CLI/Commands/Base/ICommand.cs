namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Dosh command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        void Execute();
    }
}
