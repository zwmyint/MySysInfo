using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAsyncAwait.ConsoleApp
{
    public class AsyncExample2
    {
        public async Task ProcessDataAsync()
        {
            // Simulate an async operation
            await Task.Delay(2000); // Delay for 2 second
            Console.WriteLine("Data processing completed.");
        }
    }
}
