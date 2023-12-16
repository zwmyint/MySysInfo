using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAsyncAwait.ConsoleApp
{
    public class AsyncExample3
    {
        public async Task DoSomethingAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false); // ConfigureAwait(false) for non-UI thread
            Console.WriteLine("Operation completed.");
        }
    }
}
