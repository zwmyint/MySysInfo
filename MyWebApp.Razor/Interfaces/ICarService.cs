using MyWebApp.Razor.Models;

namespace MyWebApp.Razor.Interfaces
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car? Get(int id);
        void Save(Car car);
    }
}
