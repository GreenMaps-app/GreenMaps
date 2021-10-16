using System;
using System.Net.Http;
using System.Threading.Tasks;

public interface IRestService
{
    Task<string> Get(string uri);
}

public class RestService : IRestService
{
    private HttpClient client;

    // see https://jsonplaceholder.typicode.com/
    private const string BaseUrL = "https://greenmapsapi.azurewebsites.net/api/MapLocation/1";

    public RestService()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(BaseUrL);
        client.MaxResponseContentBufferSize = 256000;
    }

    public async Task<string> Get(string uri)
    {
        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        Console.WriteLine("search");
        return response.Content.ReadAsStringAsync().Result;
    }

    public async Task<string> GetAll()
    {
        var baseUrlAll = "https://greenmapsapi.azurewebsites.net/api/MapLocation";

        return await this.Get(baseUrlAll);
    }
}