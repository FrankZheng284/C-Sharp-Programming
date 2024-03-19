namespace homework_1a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            str = Console.ReadLine();
            int a = 0, b = 0;
            char c = ' ';
            string[] arr = str.Split(' ');
            a = int.Parse(arr[0]);
            b = int.Parse(arr[1]);
            try { c = char.Parse(arr[2]); }
            catch (System.FormatException e) { Console.WriteLine("Invalid operator: {0}", arr[2]); }
            catch (System.IndexOutOfRangeException e) { Console.WriteLine("No operator"); }
            switch(c)
            { 
                case '+':
                    Console.WriteLine(a + b);
                    break;
                case '-':
                    Console.WriteLine(a - b);
                    break;
                case '*':
                    Console.WriteLine(a * b);
                    break;
                case '/':
                    Console.WriteLine((double)a / b);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }
    }
}
