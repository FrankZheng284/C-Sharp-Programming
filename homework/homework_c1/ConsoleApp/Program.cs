namespace ConsoleApp
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if(tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            for(Node<T> node = head; node != null; node = node.Next)
            {
                action(node.Data);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> intlist = new GenericList<int>();
            for(int x = 0; x < 10; x++)
            {
                intlist.Add(x);
            }
            for(Node<int> node = intlist.Head; node != null; node = node.Next)
            {
                Console.WriteLine(node.Data);
            }
            Console.WriteLine("ForEach:");
            intlist.ForEach(Console.WriteLine);
            int max = int.MinValue;
            int min = int.MaxValue;
            int sum = 0;
            intlist.ForEach(data => max = Math.Max(max, data));
            Console.WriteLine("Max: " + max);
            intlist.ForEach(data => min = Math.Min(min, data));
            Console.WriteLine("Min: " + min);
            intlist.ForEach(data => sum += data);
            Console.WriteLine("Sum: " + sum);

            GenericList<string> strlist = new GenericList<string>();
            for (int x = 0; x < 10; x++)
            {
                strlist.Add("str" + x);
            }
            for(Node<string> node = strlist.Head; node != null; node = node.Next)
            {
                Console.WriteLine(node.Data);
            }

            Console.WriteLine("ForEach:");
            strlist.ForEach(Console.WriteLine);
        }
    }
}
