using System;

namespace _1;

public class _5
{
    /**
     * Write a console application in C# to find the length of a string without
     * using an existing library function. Check your result by comparing it to
     * the string.Length() method.
     */
    public static void LengthOfAString(string[] _)
    {
        Console.Write("Enter a word: ");
        string input = Console.ReadLine() ?? throw new Exception("Input can not be null");
        int i = 0;

        try
        {
            for (; input[i] != 0; i++)
            {
                // ignored
            }
        }
        catch
        {
            // ignored
        }

        Console.WriteLine($"Length: self calculated {i}, length {input.Length}");
    }
}