using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Razor.Interfaces;

namespace MyWebApp.Razor.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        
        private readonly IDevice _device;
        private readonly ISayMyName _sayMyName;

        public double Temperature { get; set; }
        public string? MyName { get; set; }

        public PrivacyModel(ILogger<PrivacyModel> logger, IDevice device, ISayMyName sayMyName)
        {
            _logger = logger;
            _device = device;
            _sayMyName = sayMyName;
        }

        public void OnGet()
        {
            Temperature = _device.GetCpuTemperature();
            MyName = _sayMyName.IAmName();
        }
    }
}