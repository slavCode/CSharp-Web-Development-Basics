namespace ShopHierarchy
{
    using System;
    using System.Linq;
    using ShopHierarchy.Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new ShopDbContext())
            {
                ClearDatabase(context);
                FillSalesman(context);
                AddItems(context);
                ReadCommands(context);
                //Print Method For The Problem 5
                //PrintSalesMenWithCustomersCount(context);
                //Print Method For The Problem 6
                //PrintCustomersWithOrdersAndReviewsCount(context);
                //Print Method For The Problem 7
                //PrintItemsCountAndTheirOrders(context);
                //Print Method For The Problem 7
                //PrintNameOrdersAndReviews(context);
                //Print Method For The Problem 8
                PrintNumberOfOrdersWithMoreThenOneItem(context);
            }
        }

        private static void ClearDatabase(ShopDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void FillSalesman(ShopDbContext context)
        {
            var salesmanNames = Console.ReadLine().Split(';');

            foreach (var name in salesmanNames)
            {
                var salesman = new Salesman() { Name = name };
                context.Salesman.Add(salesman);
            }

            context.SaveChanges();
        }

        private static void AddItems(ShopDbContext context)
        {
            var input = Console.ReadLine();
            while (true)
            {
                if (input == "END") break;

                var itemsInfo = input.Split(';');
                var itemName = itemsInfo[0];
                var itemPrice = decimal.Parse(itemsInfo[1]);

                var item = new Item()
                {
                    Name = itemName,
                    Price = itemPrice
                };
                context.Items.Add(item);
                context.SaveChanges();

                input = Console.ReadLine();
            }
        }

        private static void ReadCommands(ShopDbContext context)
        {
            var input = Console.ReadLine();
            while (true)
            {
                if (input == "END") break;

                var args = input.Split('-');
                var command = args[0];

                switch (command)
                {
                    case "register":
                        Register(context, args[1]);
                        break;

                    case "order":
                        Order(context, args[1]);
                        break;

                    case "review":
                        Review(context, args[1]);
                        break;
                }

                input = Console.ReadLine();
            }
        }

        private static void Register(ShopDbContext context, string input)
        {
            var registerInfo = input.Split(';');

            var name = registerInfo[0];
            var salesmanId = int.Parse(registerInfo[1]);

            var customer = new Customer() { Name = name };
            var result = context.Add(customer);
            var salesman = context.Salesman.FirstOrDefault(s => s.Id == salesmanId);
            salesman.Customers.Add(result.Entity);

            context.SaveChanges();
        }

        private static void Order(ShopDbContext context, string input)
        {
            var orderParts = input.Split(';');
            var customerId = int.Parse(orderParts[0]);
            var order = new Order() { CustomerId = customerId };
            for (int i = 1; i < orderParts.Length; i++)
            {
                var itemId = int.Parse(orderParts[i]);
                order.Items.Add(new ItemOrder
                {
                    ItemId = itemId
                });
            }


            context.Orders.Add(order);
            context.SaveChanges();
        }

        private static void Review(ShopDbContext context, string input)
        {
            var reviewParts = input.Split(';');
            var customerId = int.Parse(reviewParts[0]);
            var itemId = int.Parse(reviewParts[1]);

            var review = new Review
            {
                CustomerId = customerId,
                ItemId = itemId
            };

            context.Reviews.Add(review);
            context.SaveChanges();
        }

        private static void PrintSalesMenWithCustomersCount(ShopDbContext context)
        {
            var salesmen = context.Salesman
                .Select(s => new
                {
                    s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name);


            foreach (var salesman in salesmen)
            {
                var customersCount = salesman.Customers;
                Console.WriteLine($"{salesman.Name} - {customersCount} customers");
            }
        }

        private static void PrintCustomersWithOrdersAndReviewsCount(ShopDbContext context)
        {
            var customers = context.Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Name}" +
                                  $"{Environment.NewLine}Orders: {customer.Orders}" +
                                  $"{Environment.NewLine}Reviews: {customer.Reviews}");
            }
        }

        private static void PrintItemsCountAndTheirOrders(ShopDbContext context)
        {
            var id = int.Parse(Console.ReadLine());
            var customerData = context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    Orders = c.Orders
                        .Select(o => new
                        {
                            o.Id,
                            o.Items.Count
                        })
                        .OrderBy(o => o.Id),
                    Reviews = c.Reviews.Count
                })
                .FirstOrDefault();

            foreach (var order in customerData.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Count} items");
            }

            Console.WriteLine($"reviews: {customerData.Reviews}");
        }

        private static void PrintNameOrdersAndReviews(ShopDbContext context)
        {
            int customerId = int.Parse(Console.ReadLine());
            var customerData = context
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    SalesmanName = c.Salesman.Name,
                    CustomerName = c.Name,
                    OrdersCount = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerData.CustomerName}{Environment.NewLine}" +
                              $"Orders count:{customerData.OrdersCount}{Environment.NewLine}" +
                              $"Reviews: {customerData.Reviews}{Environment.NewLine}" +
                              $"Salesman: {customerData.SalesmanName}");
        }

        private static void PrintNumberOfOrdersWithMoreThenOneItem(ShopDbContext context)
        {
            int customerId = int.Parse(Console.ReadLine());
            var ordersToPrint = context
                .Orders
                .Where(o => o.CustomerId == customerId)
                .Count(o => o.Items.Count > 1);

            Console.WriteLine($"Orders: {ordersToPrint}");
        }
    }
}
