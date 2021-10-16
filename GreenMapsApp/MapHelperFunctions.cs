using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GreenMapsApp.ViewModel;
using System.Collections.ObjectModel;

namespace GreenMapsApp
{
    class MapHelperFunctions
    {
        public async Task<string> PopulateMap(Map map)
        {
            RestService restService = new RestService();
            var returnedJsonArray = await restService.GetAll();
            Console.WriteLine(returnedJsonArray);
            Console.WriteLine("populatemap");
            ObservableCollection<PinLocations> pinLocations = new ObservableCollection<PinLocations>();


            List<MapLocationDatum> parsedJsonArray = JsonConvert.DeserializeObject<List<MapLocationDatum>>(returnedJsonArray);
            Console.WriteLine(parsedJsonArray);
            Console.WriteLine("findjson");

            foreach (var json in parsedJsonArray)
            {
                Console.WriteLine(json.Id);
                Console.WriteLine("findjson");
                string parsedJson = JsonConvert.SerializeObject(json);
                JObject parsedJsonObj = JObject.Parse(parsedJson);
                
                PinLocations temp = new PinLocations { Latitude = json.Latitude, Longitude = json.Longitude};
                pinLocations.Add(temp);
            }

            foreach (var item in pinLocations)
            {
                Pin temp = new Pin();

                temp.Label = "Test";
                temp.Position = new Position(item.Latitude, item.Longitude);

                map.Pins.Add(temp);
            }

            Pin manual = new Pin();
            manual.Label = "Test";
            manual.Position = new Position(42.26855,-123.21479);

            map.Pins.Add(manual);

            return "";
        }
    }
}
