using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApp.Razor.Pages
{
    public class Index3Model : PageModel
    {
        public int ProductCount { get; set; }
        public void OnGet()
        {
            ProductCount = 0;
        }
    }
}
