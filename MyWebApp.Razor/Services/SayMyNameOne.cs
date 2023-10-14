using MyWebApp.Razor.Interfaces;

namespace MyWebApp.Razor.Services
{
    public class SayMyNameOne : ISayMyName
    {
        public string IAmName()
        {
            var name = "I am ABC!";

            return name;
        }
    }
}
