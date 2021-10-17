using GreenMapsApp.Model;
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

    // Constructor
    public RestService()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(BaseUrL);
        client.MaxResponseContentBufferSize = 256000;
    }

    // Gets a single value point by id
    public async Task<string> Get(string uri)
    {
        HttpResponseMessage response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
    }

    // Gets all pin locations from REST API
    public async Task<string> GetAll()
    {
        string baseUrlAll = "https://greenmapsapi.azurewebsites.net/api/MapLocation";

        return await Get(baseUrlAll);
    }

    // Adds pin information to REST API
    public async Task<int> Post(string String)
    {
        string baseUrlPost = "https://greenmapsapi.azurewebsites.net/api/MapLocation/add";
        StringContent content = new StringContent(String, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(baseUrlPost, content);
        int generatedIntID = int.Parse(response.Content.ReadAsStringAsync().Result);
        return generatedIntID;
    }

    // Update status of pin to the opposite of what it was
    public async Task UpdateResolved(MapLocationDatum item, System.Collections.Generic.Dictionary<MapLocationDatum, int> dictionary)
    {
        bool resolved = item.resolved;
        bool notresolved = !resolved;
        string baseUrlPut = $"https://greenmapsapi.azurewebsites.net/api/MapLocation/update?id={dictionary[item]}&status={notresolved}";

        var response = await client.PutAsync(baseUrlPut, null);
    }
}