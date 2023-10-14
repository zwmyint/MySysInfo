using MyWebApp.Razor.Interfaces;

namespace MyWebApp.Razor.Services
{
    public class SayMyNameTwo : ISayMyName
    {
        public string IAmName()
        {
            var name = "I am XYZ!";

            return name;
        }
    }
}
