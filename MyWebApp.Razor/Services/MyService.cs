using MyWebApp.Razor.Interfaces;

namespace MyWebApp.Razor.Services
{
    public class MyService : IMyService
    {
        string _currentDateTime;

        public MyService()
        {
            this._currentDateTime = DateTime.Now.ToString("MMMM-dd hh.mm.ss.ffffff");
        }

        public string GetObjectCreationDateTime
        {
            get { return this._currentDateTime; }
        }
        public void PrintMethod()
        {
            Console.WriteLine("Hello from MyService!");
        }
    }
}
