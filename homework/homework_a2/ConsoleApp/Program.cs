namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num;
            num = int.Parse(Console.ReadLine());
            for(int i = 2; i <= num; i++)
            {
                int tag = 0;
                while(num % i == 0)
                {
                    tag = 1;
                    num /= i;
                }
                if(tag == 1)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
