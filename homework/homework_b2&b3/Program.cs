using System;
namespace ConsoleApp
{
    abstract class Shape
    {
        public abstract double Area();
    }
    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override double Area()
        {
            return Width * Height;
        }
    }
    class Square : Shape
    {
        public double Side { get; set; }
        public override double Area()
        {
            return Side * Side;
        }
    }
    class Triangle : Shape
    {
        public double Edge1 { get; set; }
        public double Edge2 { get; set; }
        public double Edge3 { get; set; }
        public bool IsTriangleValid()
        {
            return Edge1 + Edge2 > Edge3 && Edge1 + Edge3 > Edge2 && Edge2 + Edge3 > Edge1;
        }
        public override double Area()
        {
            try
            {
                if (!IsTriangleValid())
                {
                    throw new ArgumentException("Invalid triangle edges");
                }
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            double p = (Edge1 + Edge2 + Edge3) / 2;
            return Math.Sqrt(p * (p - Edge1) * (p - Edge2) * (p - Edge3));
        }
    }
    class Factory
    {
        public static Shape CreateShape(string shapeType, double edge1, double? edge2, double? edge3)
        {
            switch (shapeType)
            {
                case "Rectangle":
                    try
                    {
                        return new Rectangle { Width = edge1, Height = edge2.Value };
                    }
                    catch(InvalidOperationException)
                    {
                        throw new ArgumentException("Invalid number of edges for Rectangle");
                    }
                case "Square":
                    return new Square { Side = edge1 };
                case "Triangle":
                    try 
                    {
                        return new Triangle { Edge1 = edge1, Edge2 = edge2.Value, Edge3 = edge3.Value };
                    }
                    catch(InvalidOperationException)
                    {
                        throw new ArgumentException("Invalid number of edges for Triangle");
                    }
                default:
                    throw new ArgumentException("Invalid shape type: " + shapeType);
            }   
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = new Shape[10];
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int random_num = random.Next(1, 4);
                switch(random_num)
                {
                    case(1):
                        shapes[i] = Factory.CreateShape("Rectangle", random.Next(1, 10), random.Next(1, 10), null);
                        break;
                    case(2):
                        shapes[i] = Factory.CreateShape("Square", random.Next(1, 10), null, null);
                        break;
                    case(3):
                        shapes[i] = Factory.CreateShape("Triangle", random.Next(1, 10), random.Next(1, 10), random.Next(1, 10));
                        break;
                }
            }
            double area_sum = 0;
            foreach(Shape shape in shapes)
            {
                area_sum += shape.Area();
            }
            Console.WriteLine("Total area of shapes: " + area_sum);
        }
    }
}
