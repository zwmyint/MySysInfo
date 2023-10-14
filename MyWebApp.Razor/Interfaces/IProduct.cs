using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Interfaces
{
    public interface IProduct
    {
        Task<List<Product>> GetFilteredProducts(string filter);
        Task<List<Product>> GetProducts();
    }
}
