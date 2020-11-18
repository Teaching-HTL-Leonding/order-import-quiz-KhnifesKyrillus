using Microsoft.EntityFrameworkCore;

namespace OrderImport
{
    public class OrderImportContext : DbContext
    {
        public OrderImportContext(DbContextOptions<OrderImportContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}