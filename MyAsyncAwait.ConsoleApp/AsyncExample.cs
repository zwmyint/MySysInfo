namespace MyAsyncAwait.ConsoleApp
{
    public class AsyncExample
    {
        public async Task<string> FetchDataAsync()
        {
            // Simulate an HTTP request
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
