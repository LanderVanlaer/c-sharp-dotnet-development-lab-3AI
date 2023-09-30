using System;

namespace _1;

public class _7
{
    /**
     * Write a console application in C# to check whether a number is prime or not using recursion.
     */
    public static void PrimeRecursive(string[] _)
    {
        Console.Write("Enter a number: ");
        int input = int.Parse(Console.ReadLine() ?? throw new Exception("Input can not be null"));
        Console.WriteLine($"{input} is {(IsPrime(input) ? null : "not ")}a prime number");
    }

    private static bool IsPrime(int n, int divider = 3)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (divider > n / 2) return true;
        if (n % 2 == 0) return false;
        if (n % divider == 0) return false;
        return IsPrime(n, divider + 2);

        // or:
        // return n > 1 && (n <= 3 || divider > n / 2 || n % 2 != 0 && n % divider != 0 && IsPrime(n, divider + 2));
    }
}