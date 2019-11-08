using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Calculate(ulong number, CancellationTokenSource tokenSource)
        {
            ulong sum = 0;
            for (ulong i = 1; i <= number; i++)
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                sum += i;
            }
            Console.WriteLine($"\nСумма чисел {number} равен {sum}\n");
            Console.Write("Введите число(0 для выхода): ");
        }
        static async void CalculateAsync(ulong number, CancellationTokenSource tokenSource)
        {
            Console.Write($"Производится вычисление {number} .... ");
            await Task.Run(() => Calculate(number, tokenSource)); 
        }

        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Console.Write("Введите число(0 для выхода): ");
            ulong number = ulong.Parse(Console.ReadLine());
            while (number != 0)
            {
                CalculateAsync(number, tokenSource);
                number = ulong.Parse(Console.ReadLine());
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = new CancellationTokenSource();
            }
        }
    }
}
