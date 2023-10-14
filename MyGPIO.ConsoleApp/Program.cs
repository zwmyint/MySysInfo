using System.Device.Gpio;

namespace MyGPIO.ConsoleApp
{
    class Program
    {
        //
        // a list of all the pins used to display the value
        // pins are listed in the most significant bit order
        // i.e. pin 18 represents the highest value
        static int[] _pins = { 18, 23, 24, 25, 8, 7, 12, 16, 20, 21 };
        static GpioController _controller = new GpioController();
        //

        static void Main(string[] args)
        {
            //// Simple Flashing Sample
            //// -----------------------------------------------------
            //// GPIO 17 which is physical pin 11
            //int ledPin1 = 17;
            //GpioController controller = new GpioController();
            //// Sets the pin to output mode so we can switch something on
            //controller.OpenPin(ledPin1, PinMode.Output);

            //int lightTimeInMilliseconds = 1000;
            //int dimTimeInMilliseconds = 200;

            //while (true)
            //{
            //    Console.WriteLine($"LED1 on for {lightTimeInMilliseconds}ms");
            //    // turn on the LED
            //    controller.Write(ledPin1, PinValue.Low);
            //    Thread.Sleep(lightTimeInMilliseconds);
            //    Console.WriteLine($"LED1 off for {dimTimeInMilliseconds}ms");
            //    // turn off the LED
            //    controller.Write(ledPin1, PinValue.High);
            //    Thread.Sleep(dimTimeInMilliseconds);
            //}

            //// -----------------------------------------------------

            // Implementing a Binary Display
            // -----------------------------------------------------

            // Set all the pins to output mode and
            // ensure all the LEDs are off
            foreach (var pin in _pins)
            {
                _controller.OpenPin(pin, PinMode.Output);
                _controller.Write(pin, PinValue.High);
            }

            // play around with this value.
            //   0 will result in a virtually instant count
            //  10 looks cool
            int countDelay = 100;
            int value = 0;

            // 1 << 2 is a bit shift operation
            // 1 << 2 == 1 * 2 * 2 == 4
            // so 1 << _pins.Length represents the max number we can display + 1
            while (value < (1 << _pins.Length))
            {
                Console.WriteLine(value);
                DisplayValue(value++);
                Thread.Sleep(countDelay);
            }


            // -----------------------------------------------------


            //// Reading Input
            //// -----------------------------------------------------
            //// Set all the pins to output mode and
            //// ensure all the LEDs are off
            //foreach (var pin in _pins)
            //{
            //    _controller.OpenPin(pin, PinMode.Output);
            //    _controller.Write(pin, PinValue.High);
            //}

            //// Set the pin mode. We are using InputPullDOwn which uses a pulldown resistor to
            //// ensure the input reads Low until an external switch sets it high,
            //// We could have used InputPullUp if we wanted it to read High until it was pulled down Low.
            //// If we just used Input, the value on the pin would wander between High and Low... not very useful in this situation.
            //_controller.OpenPin(_switchPin, PinMode.InputPullDown);

            //// play around with this value.
            ////   0 will result in a virtually instant count
            ////  10 looks cool
            //int countDelay = 100;
            //int value = 0;

            //// We'll loop forever now - CTRL + C to exit
            //while (true)
            //{
            //    Console.WriteLine(value);
            //    // _pins[10-currentBit] accesses the pin number for the current bit
            //    // (value & 1<<currentBit) performs a bitwise AND operation to check if the bit is set in the value
            //    // If the bit is set, the ternary expression sets the PinValue.Low (on) or if not, PinValue.High (off)
            //    DisplayValue(value);
            //    // don't keep incrementing the value once we hit max
            //    if (value < (1 << (_pins.Length + 1)) - 1)
            //    {
            //        value++;
            //    }

            //    // Check input value each time we loop
            //    if (_controller.Read(_switchPin) == PinValue.High)
            //    {
            //        // button pressed
            //        value = 0;
            //    }

            //    Thread.Sleep(countDelay);
            //}
            //// -----------------------------------------------------
        }


        static void DisplayValue(int value)
        {
            var currentBit = _pins.Length;
            while (currentBit > 0)
            {
                // _pins[10-currentBit] accesses the pin number for the current bit
                // (value & 1< 0 ? PinValue.Low : PinValue.High);
                currentBit--;
            }
        }


        //
    }
}