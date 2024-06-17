using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace OrderManagementSystem.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService orderService;

        [TestInitialize]
        public void Setup()
        {
            orderService = new OrderService();
        }

        [TestMethod]
        public void AddOrderTest()
        {
            // Simulate user input for adding an order
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            // Add further assertions to validate the order addition
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            // Setup initial data
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            // Simulate user input for deleting an order
            using (var sw = new StringWriter())
            using (var sr = new StringReader("0\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.DeleteOrder();
            }
            // Validate the deletion
        }

        [TestMethod]
        public void ModifyOrderTest()
        {
            // Setup initial data
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            // Simulate user input for modifying an order
            using (var sw = new StringWriter())
            using (var sr = new StringReader("0\nProductName\nM\nNewProductName\n150.0\n3\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.ModifyOrder();
            }
            // Validate the modification
        }

        [TestMethod]
        public void QueryOrdersTest()
        {
            // Setup initial data
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            // Simulate user input for querying orders
            using (var sw = new StringWriter())
            using (var sr = new StringReader("\nCustomerName\nProductName\n200.0\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.QueryOrders();
            }
            // Validate the query results
        }

        [TestMethod]
        public void ExportOrdersTest()
        {
            string filePath = "test_orders.xml";
            // Add some orders
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            // Export orders
            orderService.ExportOrders(filePath);
            Assert.IsTrue(File.Exists(filePath));
            File.Delete(filePath);
        }

        [TestMethod]
        public void ImportOrdersTest()
        {
            string filePath = "test_orders.xml";
            // Add some orders and export them first
            using (var sw = new StringWriter())
            using (var sr = new StringReader("CustomerName\nProductName\n100.0\n2\nN\n"))
            {
                Console.SetOut(sw);
                Console.SetIn(sr);
                orderService.AddOrder();
            }
            orderService.ExportOrders(filePath);
            // Create a new instance of OrderService to test import
            OrderService newOrderService = new OrderService();
            newOrderService.ImportOrders(filePath);
            Assert.IsTrue(newOrderService.QueryOrders().Count > 0); // Assuming QueryOrders method returns the list of orders
            File.Delete(filePath);
        }
    }
}
