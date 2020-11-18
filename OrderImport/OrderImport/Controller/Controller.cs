using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderImport
{
    public class Controller
    {
        private readonly string[] _args;
        private readonly OrderImportContextFactory _factory;

        public Controller(string[] args)
        {
            var factory = new OrderImportContextFactory();
            _args = args;
            _factory = factory;
        }

        public async Task Delete()
        {
            var context = _factory.CreateDbContext(_args);
            context.Customers.RemoveRange(context.Customers);
            context.Orders.RemoveRange(context.Orders);
            await context.SaveChangesAsync();
        }

        public async Task Import(string[] customerText, string[] orderText)
        {
            var context = _factory.CreateDbContext(_args);
            foreach (var customer in customerText.Skip(1).ToList())
            {
                var newCustomer = new Customer
                {
                    Name = customer.Split("\t")[0],
                    CreditLimit = decimal.Parse(customer.Split("\t")[1])
                };
                await context.Customers.AddAsync(newCustomer);
            }

            await context.SaveChangesAsync();
            var customers = await context.Customers.ToListAsync();
            foreach (var customer in customers)
            {
                List<Order> orders = new();
                foreach (var order in orderText.Skip(1).ToList())
                {
                    if (!order.Split("\t")[0].Equals(customer.Name)) continue;

                    var newOrder = new Order
                    {
                        OrderDate = DateTime.Parse(order.Split("\t")[1]),
                        OrderValue = int.Parse(order.Split("\t")[2]),
                        Customer = customer,
                        CustomerId = customer.Id
                    };
                    orders.Add(newOrder);
                    await context.Orders.AddAsync(newOrder);
                }

                customer.Orders = orders;
            }

            await context.SaveChangesAsync();
        }

        public async Task Check()
        {
            var context = _factory.CreateDbContext(_args);
            var customers = await context.Customers.ToListAsync();
            var orders = await context.Orders.ToListAsync();
            foreach (var customer in customers)
            {
                var sumOfOrderValue = orders.Where(x => customer.Id == x.CustomerId).Sum(x => x.OrderValue);
                if (sumOfOrderValue > customer.CreditLimit)
                    Console.WriteLine($"{customer.Name}:\tLimit: {customer.CreditLimit} OrderValue: {sumOfOrderValue}");
            }
        }
    }
}