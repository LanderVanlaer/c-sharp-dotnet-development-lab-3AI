using System;

namespace _1;

public abstract class _1
{
    public static void Hello(string[] args)
    {
        string name;
        if (args.Length >= 1)
        {
            name = args[0];
        }
        else
        {
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
        }

        Console.WriteLine($"Hello {name}!");
    }
}