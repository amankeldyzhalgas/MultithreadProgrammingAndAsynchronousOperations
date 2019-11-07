using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Calculate(ulong number)
        {
            ulong sum = 0;
            for (ulong i = 1; i <= number; i++)
            {
                sum += i;
            }
            Console.WriteLine($"Сумма чисел {number} равен {sum}");
            Console.Write("Введите число(0 для выхода): ");
        }
        static async void CalculateAsync(ulong number)
        {
            await Task.Run(() => Calculate(number)); 
        }

        static void Main(string[] args)
        {
            Console.Write("Введите число(0 для выхода): ");
            ulong number = ulong.Parse(Console.ReadLine());
            while (number != 0)
            {
                CalculateAsync(number);
                number = ulong.Parse(Console.ReadLine());
            }

            Console.Read();
        }
    }
}
