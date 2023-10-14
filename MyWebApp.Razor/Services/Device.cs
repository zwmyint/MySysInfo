using Iot.Device.CpuTemperature;
using MyWebApp.Razor.Interfaces;
using UnitsNet;

namespace MyWebApp.Razor.Services
{
    public class Device : IDevice
    {
        CpuTemperature _temperature;

        public Device()
        {
            _temperature = new CpuTemperature();
        }

        public double GetCpuTemperature()
        {
            try
            {
                if (_temperature.IsAvailable)
                {
                    return Math.Round(_temperature.Temperature.DegreesCelsius, 2);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
