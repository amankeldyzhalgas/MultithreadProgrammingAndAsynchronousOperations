// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Program.
    /// </summary>
    internal class Program
    {
        private static void Calculate(ulong number, CancellationTokenSource tokenSource)
        {
            ulong sum = 0;
            for (ulong i = 1; i <= number; i++)
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation aborted by token.");
                    return;
                }

                sum += i;
            }

            Console.WriteLine($"The sum of numbers from 0 to {number} is equal to {sum}\n");
            Console.Write("Please enter the number(0 to exit): ");
        }

        private static async void CalculateAsync(ulong number, CancellationTokenSource tokenSource)
        {
            Console.WriteLine($"Processing...The sum of numbers from 0 to {number} is calculating. ");
            await Task.Run(() => Calculate(number, tokenSource));
        }

        private static ulong GetNumber()
        {
            ulong number = 1;
            bool result = false;
            while (!result)
            {
                result = ulong.TryParse(Console.ReadLine(), out number);
            }

            return number;
        }

        private static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Console.Write("Please enter an integer number(0 to exit): ");
            ulong number = GetNumber();

            while (number != 0)
            {
                CalculateAsync(number, tokenSource);
                number = GetNumber();
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = new CancellationTokenSource();
            }
        }
    }
}
