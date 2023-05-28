using Microsoft.EntityFrameworkCore;

namespace LI2.Pages.Models
{
    public class LI2Context : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StatusOfOrder> StatusOfOrders { get; set; }
        public virtual DbSet<TypeOfProduct> TypeOfProducts { get; set; }
        public virtual DbSet<CategoryOfProduct> CategoryOfProducts { get; set; }
        public virtual DbSet<OrderToProduct> OrderToProducts { get; set; }
        public LI2Context(DbContextOptions<LI2Context> options) : base(options) { Database.EnsureCreated(); }
    }
}
