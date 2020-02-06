namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Command base class
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Execute command.
        /// </summary>
        public void Execute()
        {
            OnExecute();
        }

        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected abstract void OnExecute();
    }
}
