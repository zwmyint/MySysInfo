
namespace MyAsyncAwait.ConsoleApp
{
    public class Program
    {
        //https://blog.stackademic.com/best-practices-for-using-async-await-in-c-with-net-core-b067ea3fa9d3
        public static async Task Main(string[] args)
        {
            // 1
            var example1 = new AsyncExample();
            var result1 = await example1.FetchDataAsync();
            Console.WriteLine(result1);

            // 2
            var example2 = new AsyncExample2();
            await example2.ProcessDataAsync();

            // 3
            var example3 = new AsyncExample3();
            await example3.DoSomethingAsync();









            //
        }

    }
}