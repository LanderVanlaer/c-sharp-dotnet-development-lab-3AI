using System;

namespace _1;

public class _3
{
    /**
     * Write a C# console application to print on screen the output of
     * adding, subtracting, multiplying and dividing two numbers which will be entered by the user.
     *
     * Create a separate class with private methods for the calculations.
     * A public method that accepts 2 parameters that will call the calculation methods.
     */
    public static void Calculations(string[] _)
    {
        Console.Write("Enter the first number: ");
        int num1 = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("Enter the Second number: ");
        int num2 = int.Parse(Console.ReadLine() ?? string.Empty);

        Console.WriteLine($"{num1} + {num2} = {Add(num1, num2)}");
        Console.WriteLine($"{num1} - {num2} = {Subtract(num1, num2)}");
        Console.WriteLine($"{num1} * {num2} = {Multiply(num1, num2)}");
        Console.WriteLine($"{num1} / {num2} = {Divide(num1, num2)}");
    }

    private static int Add(int num1, int num2)
    {
        return num1 + num2;
    }

    private static int Subtract(int num1, int num2)
    {
        return num1 - num2;
    }

    private static int Multiply(int num1, int num2)
    {
        return num1 * num2;
    }

    private static double Divide(int num1, int num2)
    {
        return (double)num1 / num2;
    }
}