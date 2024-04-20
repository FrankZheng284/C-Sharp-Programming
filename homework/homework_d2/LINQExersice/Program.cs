using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQExersice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<int> nums = new List<int>();
            for(int i = 0; i < 100; i++)
            {
                nums.Add(random.Next(1000));
            }
            var sorted_nums = nums.OrderBy(n => n);
            var avg = sorted_nums.Average();
            var sum = sorted_nums.Sum();
            Console.WriteLine("The sorted numbers are: ");
            Console.WriteLine(string.Join(", ", sorted_nums));
            Console.WriteLine("The average is: {0}", avg);
            Console.WriteLine("The sum is: {0}", sum);

        }
    }
}
