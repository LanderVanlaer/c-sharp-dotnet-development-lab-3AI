using System;

namespace _1;

public class _9
{
    /**
     * FizzBuzz is a group word game for children to teach them about division. Players take turns to count
     * incrementally, replacing any number divisible by three with the word fizz, any number divisible by five with the
     * word buzz, and any number divisible by both with fizzbuzz.
     *
     * Some interviewers give applicants simple FizzBuzz-style problems to solve during interviews. Most good
     * programmers should be able to write out on paper or whiteboard a program to output a simulated FizzBuzz game in
     * under a couple of minutes.
     * Want to know something worrisome? Many computer science graduates can't.
     *
     * You can even find senior programmers who take more than 10-15 minutes to write a solution.
     *
     * Create a console application that outputs a simulated FizzBuzz game counting up to 100. The output should look
     * something like the following screenshot:
     * <code>
     * 1, 2, fizz, 4, buzz, fizz, 7, 8, fizz, buzz,
     * 11, fizz, 13, 14, fizzbuzz, 16, 17, fizz, 19, buzz,
     * fizz, 22, 23, fizz, buzz, 26, fizz, 28, 29, fizzbuzz,
     * 31, 32, fizz, 34, buzz, fizz, 37, 38, fizz, buzz,
     * 41, fizz, 43, 44, fizzbuzz, 46, 47, fizz, 49, buzz,
     * fizz, 52, 53, fizz, buzz, 56, fizz, 58, 59, fizzbuzz,
     * 61, 62, fizz, 64, buzz, fizz, 67, 68, fizz, buzz,
     * 71, fizz, 73, 74, fizzbuzz, 76, 77, fizz, 79, buzz,
     * fizz, 82, 83, fizz, buzz, 86, fizz, 88, 89, fizzbuzz,
     * 91, 92, fizz, 94, buzz, fizz, 97, 98, fizz, buzz,
     * </code>
     *
     * Change the application so that the user can input the fizz and buzz numbers and the maximum count at the start
     * of the application.
     */
    public static void FizzBuzz(string[] _)
    {
        Console.Write("Enter your fizz number (3): ");
        string input = Console.ReadLine();
        int fizz = input is null || input.Length == 0 ? 3 : int.Parse(input);

        Console.Write("Enter your buzz number (5): ");
        input = Console.ReadLine();
        int buzz = input is null || input.Length == 0 ? 5 : int.Parse(input);

        Console.WriteLine();

        for (int i = 1; i <= 100; i++)
        {
            if (i % fizz == 0)
            {
                Console.Write("fizz");
                if (i % buzz == 0) Console.Write("buzz");
            }
            else if (i % buzz == 0)
            {
                Console.Write("buzz");
            }
            else
            {
                Console.Write(i);
            }

            Console.Write(", ");
            if (i % 10 == 0)
                Console.WriteLine();
        }
    }
}