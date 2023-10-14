using Microsoft.EntityFrameworkCore;
using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Data
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

    }
}
