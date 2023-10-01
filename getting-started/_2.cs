using System;

namespace _1;

public class _2
{
    /**
     * Write a C# console application that takes a number as input and print its multiplication table.
     * Make sure your application does not crash and that you get an error message when the input is not a number.
     */
    public static void MultiplicationTable(string[] args)
    {
        Console.Write("Enter a number: ");
        int num = int.Parse(Console.ReadLine() ?? string.Empty);

        for (int i = 0; i <= 10; i++)
            Console.WriteLine(
                $"{num} * {i,2} = {(num * i).ToString().PadLeft((int)Math.Floor(Math.Log10(num) + 2), ' ')}");
    }
}