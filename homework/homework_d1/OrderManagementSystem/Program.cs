using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace OrderManagementSystem
{
    class Product
    {
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public override bool Equals(object? obj)
        {
            return ProductName == ((Product)obj).ProductName;
        }
        public override string ToString()
        {
            return $"Product: {ProductName}, Price: {ProductPrice}";
        }
    }
    class Customer
    {
        public string CustomerName { get; set; }
        public override string ToString()
        {
            return $"Customer: {CustomerName}";
        }
    }
    class OrderDetails
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public override bool Equals(object? obj)
        {
            return Product.Equals(((OrderDetails)obj).Product);
        }
        public override string ToString()
        {
            return $"{Product.ToString()}, Quantity: {Quantity}";
        }
    }
    class Order
    {
        private static int _orderId = 0;
        public int OrderId { get; private set; }
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; }
        public double TotalAmount => Details.Sum(x => x.Product.ProductPrice * x.Quantity);

        public Order()
        {
            OrderId = _orderId++;
            Details = new List<OrderDetails>();
        }
        public override bool Equals(object? obj)
        {
            return OrderId == ((Order)obj).OrderId;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order ID: {OrderId}");
            sb.AppendLine(Customer.ToString());
            sb.AppendLine("Order Details:");
            foreach (var item in Details)
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine($"Total Amount: {TotalAmount}");
            return sb.ToString();
        }
    }
    class OrderService
    {
        private List<Order> orders = new List<Order>();
        public void AddOrder()
        {
            Console.Clear();
            Order order = new Order();
            Console.Write("Please enter the customer's name: ");
            order.Customer = new Customer() { CustomerName = Console.ReadLine() };
            while (true)
            {
                Console.Write("Please enter the product's name: ");
                string productName = Console.ReadLine();
                Console.Write("Please enter the product's price: ");
                double productPrice = double.Parse(Console.ReadLine());
                Console.Write("Please enter the quantity of the product: ");
                int quantity = int.Parse(Console.ReadLine());
                OrderDetails orderDetails = new OrderDetails()
                {
                    Product = new Product() { ProductName = productName, ProductPrice = productPrice },
                    Quantity = quantity
                };
                if (order.Details.Contains(orderDetails))
                {
                    Console.WriteLine("The product already exists in the order.");
                    continue;
                }
                order.Details.Add(orderDetails);
                Console.Write("Do you want to add more products (Y/N): ");
                string choice = Console.ReadLine();
                if(string.IsNullOrEmpty(choice) || choice.ToUpper() == "N")
                {
                    break;
                }
            }
            orders.Add(order);
        }
        public void DeleteOrder()
        {
            Console.Clear();
            Console.WriteLine("Please enter the order ID you want to delete: ");
            try
            {
                int orderId = int.Parse(Console.ReadLine());
                Order? order = orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order == null)
                {
                    Console.WriteLine("The order does not exist.");
                    return;
                }
                orders.Remove(order);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input. Please enter an integer order ID.");
            }
        }
        public void ModifyOrder()
        {
            Console.Clear();
            Console.Write("Please enter the order ID you want to modify: ");
            try
            {
                int orderId = int.Parse(Console.ReadLine());
                Order? order = orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order == null)
                {
                    Console.WriteLine("The order does not exist.");
                    Console.ReadLine();
                    return;
                }
                Console.Write("Please enter the product name you want to modify: ");
                string productName = Console.ReadLine();
                OrderDetails? orderDetails = order.Details.FirstOrDefault(x => x.Product.ProductName == productName);
                if (orderDetails == null)
                {
                    Console.WriteLine("The product does not exist in the order.");
                    return;
                }
                Console.WriteLine("The product details are as follows:");
                Console.WriteLine(orderDetails.ToString());
                Console.WriteLine("Are you going to modify or delete the product? (M/D)");
                string choice = Console.ReadLine();
                if(string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid input. Please enter M or D.");
                    return;
                }
                if (choice.ToUpper() == "M")
                {
                    Console.Write("Please enter the new product name, if you don't want to change it, tap \"Enter\": ");
                    string newProductName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newProductName))
                    {
                        orderDetails.Product.ProductName = newProductName;
                    }
                    Console.Write("Please enter the new product price, if you don't want to change it, tap \"Enter\": ");
                    string newProductPrice = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newProductPrice))
                    {
                        orderDetails.Product.ProductPrice = double.Parse(newProductPrice);
                    }
                    Console.Write("Please enter the new quantity, if you don't want to change it, tap \"Enter\": ");
                    string newQuantity = Console.ReadLine();
                    try 
                    { 
                        orderDetails.Quantity = int.Parse(newQuantity);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input. Please enter an integer quantity.");
                        Console.ReadLine();
                    }
                }
                else if (choice.ToUpper() == "D")
                {
                    order.Details.Remove(orderDetails);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter M or D.");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input. Please enter an integer order ID.");
                Console.ReadLine();
            }
        }
        public void QueryOrders()
        {
            List<Order> queriedOrders = orders;
            Console.Clear();
            Console.WriteLine("Please enter the imformation of the orders you want to query, tap \"Enter\" to skip: ");
            Console.Write("Order ID: ");
            string orderId = Console.ReadLine();
            if(!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    queriedOrders = queriedOrders.Where(x => x.OrderId == int.Parse(orderId)).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please enter an integer order ID.");
                    Console.ReadLine();
                    return;
                }
            }
            Console.Write("Customer Name: ");
            string customerName = Console.ReadLine();
            if (!string.IsNullOrEmpty(customerName))
            {
                queriedOrders = queriedOrders.Where(x => x.Customer.CustomerName == customerName).ToList();
            }
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            if (!string.IsNullOrEmpty(productName))
            {
                queriedOrders = queriedOrders.Where(x => x.Details.Any(y => y.Product.ProductName == productName)).ToList();
            }
            Console.Write("Total Price: ");
            string totalPrice = Console.ReadLine();
            if (!string.IsNullOrEmpty(totalPrice))
            {
                try
                {
                    queriedOrders = queriedOrders.Where(x => x.TotalAmount == double.Parse(totalPrice)).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please enter a double total price.");
                    Console.ReadLine();
                    return;
                }
            }

            //if(queriedOrders.Count == 0)
            //{
            //    Console.WriteLine("No orders found.");
            //    Console.ReadLine();
            //    return;
            //}
            Console.WriteLine("The orders found are as follows:");
            foreach (var order in queriedOrders)
            {
                Console.WriteLine(order.ToString());
            }
            Console.ReadLine();
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Order Management System");
                Console.WriteLine("1. Add Order");
                Console.WriteLine("2. Delete Order");
                Console.WriteLine("3. Modify Order");
                Console.WriteLine("4. Query Orders");
                Console.WriteLine("5. Exit");
                Console.Write("Please enter your choice: ");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        orderService.AddOrder();
                        break;
                    case "2":
                        orderService.DeleteOrder();
                        break;
                    case "3":
                        orderService.ModifyOrder();
                        break;
                    case "4":
                        orderService.QueryOrders();
                        break;
                    case "5":
                        Console.WriteLine("See you next time.");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number from 1 to 5.");
                        Console.WriteLine("Press any key to continue.");
                        break;
                }

            }
        }
    }
}
