using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderImport
{
    public class Controller
    {
        private OrderImportContext context;

        public Controller(string[] args)
        {
            var factory = new OrderImportContextFactory();
            using OrderImportContext dbContext = factory.CreateDbContext(args);
            context = dbContext;
        }

        public async Task Delete()
        {
            context.Customers.RemoveRange(context.Customers);
            context.Orders.RemoveRange(context.Orders);
            await context.SaveChangesAsync();
        }

        public async Task Import(string[] customerText, string[] orderText)
        {
            foreach (var customer in customerText.Skip(1).ToList())
            {
                var newCustomer = new Customer
                {
                    Name = customer.Split("\t")[0],
                    CreditLimit = Decimal.Parse(customer.Split("\t")[1])
                };
                List<Order> orders = new();
                foreach (var order in orderText.Skip(1).ToList())
                {
                    if (!order.Split("\t")[0].Equals(newCustomer.Name))
                    {
                        continue;
                    }

                    var newOrder = new Order
                    {
                        OrderDate = DateTime.Parse(order.Split("\t")[1]),
                        OrderValue = int.Parse(order.Split("\t")[2]),
                        Customer = newCustomer
                    };
                    orders.Add(newOrder);
                }

                newCustomer.Orders = orders;
                await context.Customers.AddAsync(newCustomer);
            }

            await context.SaveChangesAsync();
        }

        public async Task Check()
        {
            var customers = await context.Customers.ToListAsync();
            var orders = await context.Orders.ToListAsync();
            foreach (var customer in customers)
            {
                var sumOfOrderValue = orders.Where(x => customer.Id == x.CustomerId).Sum(x => x.OrderValue);
                if (sumOfOrderValue > customer.CreditLimit)
                {
                    Console.WriteLine($"{customer.Name}:\t{customer.CreditLimit}/{sumOfOrderValue}");
                }
            }
        }
    }
}