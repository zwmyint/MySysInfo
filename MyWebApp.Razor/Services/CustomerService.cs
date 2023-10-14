using Microsoft.EntityFrameworkCore;
using MyWebApp.Razor.Data;
using MyWebApp.Razor.Interfaces;
using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Services
{
    public class CustomerService : ICustomer
    {
        private readonly NorthwindContext context;

        public CustomerService(NorthwindContext context) => this.context = context;

        public async Task<List<Customer>> GetFilteredCustomers(string filter)
        {
            return await context.Customers
                .Where(x => x.CustomerName.ToLower().Contains(filter.ToLower()))
                .OrderBy(x => x)
                .ToListAsync();
        }
    }
}
