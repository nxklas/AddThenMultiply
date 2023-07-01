using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AddThenMultiply.Commands
{
    /// <summary>
    /// Represents the base class for executable commands.
    /// </summary>
    internal abstract class Command
    {
        /// <summary>
        /// An enumerable that contains all existing commands.
        /// </summary>
        public static readonly IEnumerable<Command> All = GetAll();
        /// <summary>
        /// The exit command.
        /// </summary>
        public static readonly ExitCommand Exit = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="arguments">The arguments of the command.</param>
        protected Command(string name, params Argument[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        /// <summary>
        /// Represents the name of the command.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Represents the arguments of the command.
        /// </summary>
        public Argument[] Arguments { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="arguments">A set of arguments for execution.</param>
        /// <returns>An <see cref="int"/> instance that represents 0 when successful; otherwise, 1.</returns>
        public abstract int Execute(params string[] arguments);

        /// <summary>
        /// Gets all existing commands by using reflection. In favor of performance, it is recommended to use the <see cref="All"/> field.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Command"/>s that contains all commands.</returns>
        public static IEnumerable<Command> GetAll() => typeof(Command).GetFields(BindingFlags.Public | BindingFlags.Static)
                                                                                          .Where(f => f.FieldType.IsAssignableTo(typeof(Command)))
                                                                                          .Select(f => (Command)f.GetValue(null)!);
    }
}