using System;
using System.Net.Http;
using System.Text;
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
        HttpResponseMessage response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
    }

    public async Task<string> GetAll()
    {
        string baseUrlAll = "https://greenmapsapi.azurewebsites.net/api/MapLocation";

        return await Get(baseUrlAll);
    }

    public async void Post(string String)
    {
        string baseUrlAll = "https://greenmapsapi.azurewebsites.net/api/MapLocation/add";
        StringContent content = new StringContent(String, Encoding.UTF8, "application/json");
        Console.WriteLine("asnycsearch");
        Console.WriteLine(String);
        HttpResponseMessage response = await client.PostAsync(baseUrlAll, content);
        Console.WriteLine(response);
        Console.WriteLine("response");
    }
}