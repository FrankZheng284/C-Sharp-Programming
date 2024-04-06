namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] numberstr = input.Split(' ');
            int[] numbers = new int[numberstr.Length];
            for (int i = 0; i < numberstr.Length; i++)
            {
                numbers[i] = int.Parse(numberstr[i]);
            }
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                max = Math.Max(max, numbers[i]);
            }
            int min = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                min = Math.Min(min, numbers[i]);
            }
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }
            int avg = sum / numbers.Length;
            Console.WriteLine("Max: " + max);
            Console.WriteLine("Min: " + min);
            Console.WriteLine("Sum: " + sum);
            Console.WriteLine("Avg: " + avg);
        }
    }
}
