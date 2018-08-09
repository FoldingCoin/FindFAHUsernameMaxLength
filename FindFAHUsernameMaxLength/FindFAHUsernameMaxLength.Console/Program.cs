namespace FindFAHUsernameMaxLength.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Press <enter> to exit");
            Console.ReadLine();
        }

        private static void Run()
        {
            Console.WriteLine("Provide the path to the FAH stats text file and then press <enter>: ");

            string input = Console.ReadLine();

            if (input == null || !File.Exists(input))
            {
                Console.WriteLine($"The file path '{input}' does not exist, try again.");
                Console.WriteLine();
                Run();
            }
            else if (input.EndsWith(".bz2"))
            {
                Console.WriteLine($"Decompress the file first, try again.");
                Console.WriteLine();
                Run();
            }
            else if (!input.EndsWith(".txt"))
            {
                Console.WriteLine($"Expected the text file to have the '.txt' extension, try again.");
                Console.WriteLine();
                Run();
            }
            else
            {
                IEnumerable<string> fileLines = File.ReadLines(input);

                var names = new List<string>();

                foreach (string fileLine in fileLines)
                {
                    string currentLine = fileLine;
                    string[] unparsedUserData =
                        currentLine.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (unparsedUserData.Length != 4)
                    {
                        continue;
                    }

                    names.Add(unparsedUserData[0]);
                }

                Console.WriteLine($"The max username size found was '{names.Max(name => name.Length)}'");
                Console.WriteLine(
                    $"Found '{names.Count(name => name.Length > 50)}' users' name that exceeded 50 chars");
                Console.WriteLine(
                    $"Found '{names.Count(name => name.Length > 75)}' users' name that exceeded 75 chars");
                Console.WriteLine(
                    $"Found '{names.Count(name => name.Length > 100)}' users' name that exceeded 100 chars");
            }
        }
    }
}