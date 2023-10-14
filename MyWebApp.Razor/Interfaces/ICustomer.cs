using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Interfaces
{
    public interface ICustomer
    {
        Task<List<Customer>> GetFilteredCustomers(string filter);
    }
}
