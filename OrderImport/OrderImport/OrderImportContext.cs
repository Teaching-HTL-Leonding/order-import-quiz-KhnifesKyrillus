using Microsoft.EntityFrameworkCore;

namespace OrderImport
{
    public class OrderImportContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        public OrderImportContext(DbContextOptions<OrderImportContext> options)
            : base(options)
        {
        }
    }
}