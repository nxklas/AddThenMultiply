namespace AddThenMultiply.Commands
{
    /// <summary>
    /// Represents the exit command that exits this application whene executed.
    /// </summary>
    internal sealed class ExitCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommand"/> class.
        /// </summary>
        public ExitCommand() : base(name: "exit")
        {
        }

        /// <summary>
        /// Closes this application by executing this command.
        /// </summary>
        /// <param name="arguments">A set of arguments for execution. This command ignores that parameter.</param>
        /// <returns>An <see cref="int"/> instance that represents 0, meaning the command is executed successfully. However, this method never returns.</returns>
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
        public override object Execute(params string[] arguments)
        {
            System.Environment.Exit(0);
            return 0;
        }
    }
}