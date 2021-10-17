using GreenMapsApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    public async Task<int> Post(string String)
    {
        string baseUrlPost = "https://greenmapsapi.azurewebsites.net/api/MapLocation/add";
        StringContent content = new StringContent(String, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(baseUrlPost, content);
        int generatedIntID = int.Parse(response.Content.ReadAsStringAsync().Result);
        return generatedIntID;
    }

    public async Task UpdateResolved(MapLocationDatum item, System.Collections.Generic.Dictionary<MapLocationDatum, int> dictionary)
    {
        bool resolved = item.resolved;
        bool notresolved = !resolved;
        string baseUrlPut = $"https://greenmapsapi.azurewebsites.net/api/MapLocation/update?id={dictionary[item]}&status={notresolved}";

        var response = await client.PutAsync(baseUrlPut, null);
    }
}