using System;

namespace _1;

public class _4
{
    /**
     * Write a C# console application to find the longest word in a string.
     */
    public static void LongestWord(string[] _)
    {
        Console.Write("Enter a sentence: ");
        string input = Console.ReadLine();
        if (input is null) throw new Exception("Could not read line.");
        input = input.Trim();
        if (input.Length == 0) throw new Exception("Please enter text.");

        string[] words = input.Split(' ');
        string longestWord = words[0];

        foreach (string word in words)
            if (longestWord.Length < word.Length)
                longestWord = word;

        Console.WriteLine($"The Longest Word is: {longestWord}");
    }
}