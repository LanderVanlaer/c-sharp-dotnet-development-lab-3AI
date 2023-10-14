using System;

namespace _1;

public class _6
{
    /**
     * Write a console application in C# to print numbers from n to 1 using recursion.
     */
    public static void PrintNumbers(string[] _)
    {
        Console.Write("Enter a number: ");
        int input = int.Parse(Console.ReadLine() ?? throw new Exception("Input can not be null"));
        PrintNumbersRecursive(input);
    }

    private static void PrintNumbersRecursive(int n)
    {
        Console.Write($"{n}, ");
        if (n > 1)
            // ReSharper disable once TailRecursiveCall
            PrintNumbersRecursive(n - 1);
    }
}