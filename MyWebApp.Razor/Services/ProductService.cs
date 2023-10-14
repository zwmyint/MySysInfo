using Microsoft.EntityFrameworkCore;
using MyWebApp.Razor.Data;
using MyWebApp.Razor.Interfaces;
using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Services
{
    public class ProductService : IProduct
    {
        private readonly NorthwindContext context;

        public ProductService(NorthwindContext context) => this.context = context;

        public async Task<List<Product>> GetFilteredProducts(string filter)
        {
            return await context.Products
                .Where(x => x.ProductName.ToLower().Contains(filter.ToLower()))
                .OrderBy(x => x)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProducts() => await context.Products.ToListAsync();
    }
}
