﻿using System;

namespace _1;

public abstract class _1
{
    /**
     * Write a C# console application that asks for your name and print Hello and your name.
     * You can also give your name as an argument, in which case the user is not prompted for the name anymore.
     */
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