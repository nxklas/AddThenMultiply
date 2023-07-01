namespace AddThenMultiply.Commands
{
    /// <summary>
    /// Represents data for <see cref="Command"/> execution.
    /// </summary>
    internal readonly struct Argument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Argument"/> struct.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <param name="isOptional">Indicates whether this argument is optional.</param>
        public Argument(string name, bool isOptional)
        {
            Name = name;
            IsOptional = isOptional;
        }

        /// <summary>
        /// Represents the name of the argument.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Indicates whether the argument is optional.
        /// </summary>
        /// <returns><see langword="true"/> if the command is optional; otherwise, <see langword="false"/>.</returns>
        public bool IsOptional { get; }
    }
}