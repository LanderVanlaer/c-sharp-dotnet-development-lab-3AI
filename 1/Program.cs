using System;

namespace _1;

internal class Program
{
    private static readonly Option[] Options =
    {
        new("Hello", _1.Hello),
        new("Multiplication Table", _2.MultiplicationTable),
        new("Calculations", _3.Calculations),
        new("Longest Word", _4.LongestWord),
    };

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--------------------\n");

            for (int i = 0; i < Options.Length; i++)
                Console.WriteLine($"{i + 1:00})\t{Options[i].Name}");

            Console.WriteLine($"{99})\tEXIT");

            Console.Write("Choose: ");
            int? choice = null;
            do
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int temp))
                {
                    choice = temp;
                }
                else
                {
                    Console.Write("Only numbers are allowed: ");
                    continue;
                }

                if (choice == 99) break;
                if (choice < 1)
                {
                    Console.Write("Choice can not be less than 1: ");
                    choice = null;
                }
                else if (choice > Options.Length)
                {
                    Console.Write($"Choice can not be greater than {Options.Length}: ");
                    choice = null;
                }
            } while (choice == null);

            if (choice == 99) break;

            Console.WriteLine("\n");
            try
            {
                Options[(int)(choice - 1)].Function(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n####################\n");
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("\n####################\n");
            }
        }
    }
}

internal class Option
{
    public readonly Action<string[]> Function;
    public readonly string Name;

    public Option(string name, Action<string[]> function)
    {
        Name = name;
        Function = function;
    }
}