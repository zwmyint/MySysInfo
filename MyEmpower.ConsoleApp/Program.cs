using MyHttpClient.ConsoleApp;
using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositories = await ProcessRepositoriesAsync(client);

foreach (var repo in repositories)
{
    Console.WriteLine($"Name: {repo.Name}");
    Console.WriteLine($"Homepage: {repo.Homepage}");
    Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
    Console.WriteLine($"Description: {repo.Description}");
    Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
    Console.WriteLine($"{repo.LastPush}");
    Console.WriteLine();
}

//
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://whois-lookup1.p.rapidapi.com/imnikgoyal/whois-lookup?domain=facebook.com"),
    Headers =
    {
        { "X-RapidAPI-Key", "50a1f1092dmsh8b81b5b12ec1a08p1fa7e2jsn2cd15d818570" },
        { "X-RapidAPI-Host", "whois-lookup1.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}

var request1 = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://weatherapi-com.p.rapidapi.com/current.json?q=53.1%2C-0.13"),
    Headers =
    {
        { "X-RapidAPI-Key", "50a1f1092dmsh8b81b5b12ec1a08p1fa7e2jsn2cd15d818570" },
        { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request1))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}

//
static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
{
    await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
    return repositories ?? new();
}


