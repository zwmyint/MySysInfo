using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApp.Razor.Pages
{
    public class Index2Model : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";
        public List<string> Animals = new List<string>();
        
        public void OnGet()
        {
            Message += $" Server time is {DateTime.Now}";
            Animals.AddRange(new[] { "Antelope", "Badger", "Cat", "Dog" });
        }
    }
}
