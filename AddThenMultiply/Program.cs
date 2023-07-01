using AddThenMultiply.Commands;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AddThenMultiply
{
    /// <summary>
    /// Provides the main entry point for the application.
    /// </summary>
    internal static class Program
    {
        private const ushort DefaultThreshold = 4;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine($"Enter two positive values, or a third one to define a threshold, separated by a comma.{Environment.NewLine}");
                var input = Console.ReadLine();

                if (input == null)
                {
                    Console.Clear();
                    Console.WriteLine($"The console has been cleared because there were no more lines available.{Environment.NewLine}" +
                        $"Please re-enter.");
                    continue;
                }

                if (input.StartsWith('#'))
                {
                    var splittedInput = Split(input[1..], ' ');

                    if (splittedInput.Length == 0)
                    {
                        Console.WriteLine($"No command is followed by the '#'.{Environment.NewLine}");
                        continue;
                    }

                    var commandName = splittedInput[0].ToLower();
                    var command = Command.All.FirstOrDefault(c => c.Name == commandName);

                    if (command == default)
                    {
                        Console.WriteLine($"The command \"{commandName}\" could not be found. Please try again.{Environment.NewLine}");
                        continue;
                    }

                    command.Execute(splittedInput[1..]);
                    Console.WriteLine();
                    continue;
                }

                var values = Split(input, ',');

                if (values.Length < 2 || values.Length > 3)
                {
                    Console.WriteLine($"Please enter two OR three comma-separated values.{Environment.NewLine}");
                    continue;
                }

                var error = false;

                if (!ushort.TryParse(values[0], out var start1))
                {
                    Console.WriteLine("The first value was not in the format of an unsigned int.");
                    error = true;
                }

                if (!ushort.TryParse(values[1], out var start2))
                {
                    Console.WriteLine("The second value was not in the format of an unsigned int.");
                    error = true;
                }

                if (error)
                {
                    Console.WriteLine($"Please re-enter in the correct format.{Environment.NewLine}");
                    continue;
                }

                var threshold = DefaultThreshold;
                var parseFailed = false;

                if (values.Length == 3 && !ushort.TryParse(values[2], out threshold))
                {
                    Console.WriteLine($"The third value was not in the format of an unsigned int, the default threshold ({DefaultThreshold}) will be used instead.");
                    parseFailed = true;
                }

                var value = Run(start1, start2, parseFailed ? DefaultThreshold : threshold);

                Console.WriteLine($"The output is: {value}.{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Adds and multiplies alternately two figures till a specified threshold is reached.
        /// </summary>
        /// <param name="start1">The first figure.</param>
        /// <param name="start2">The second figure.</param>
        /// <param name="threshold">The threshold that limits this process's interations.</param>
        /// <returns>An <see cref="ulong"/> instance that represents the value that is made when <paramref name="start1"/>
        /// and <paramref name="start2"/> are alternately added and multiplied to each other, starting with addition.
        /// Once this process is repeated <paramref name="threshold"/> times, the current value is returned.</returns>
        private static ulong Run(ushort start1, ushort start2, ushort threshold)
        {
            if (threshold == 0)
                return start1;

            ulong s1 = start1;
            ulong s2 = start2;
            var isEven = false;

            for (ushort i = 0; i < threshold; i++)
            {
                isEven = i % 2 == 0;

                if (isEven)
                    s1 += s2;
                else
                    s2 *= s1;
            }

            return isEven ? s1 : s2;
        }

        /// <summary>
        /// Splits a string into substrings based on a specified delimiting character and removes empty entries and leading and trailing white-space characters.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <param name="separator">A character that delimits the substrings.</param>
        /// <returns>A <see cref="string"/>[] whose elements contain the substrings from <paramref name="str"/> that are delimited by <paramref name="separator"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string[] Split(string str, char separator) => str.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}