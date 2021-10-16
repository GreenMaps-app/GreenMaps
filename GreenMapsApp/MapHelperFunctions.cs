using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GreenMapsApp.Model;
using System.Collections.ObjectModel;
using Plugin.Geolocator;

namespace GreenMapsApp
{
    class MapHelperFunctions
    {
        public async void PopulateMap(Map map)
        {
            RestService restService = new RestService();
            var returnedJsonArray = await restService.GetAll();
            ObservableCollection<PinLocations> pinLocations = new ObservableCollection<PinLocations>();


            List<MapLocationDatum> parsedJsonArray = JsonConvert.DeserializeObject<List<MapLocationDatum>>(returnedJsonArray);

            foreach (MapLocationDatum json in parsedJsonArray)
            {
                string parsedJson = JsonConvert.SerializeObject(json);

                Pin pin = new Pin
                {
                    Label = "Test",
                    Position = new Position(json.latitude, json.longitude),
                    Address = json.message
                };

                map.Pins.Add(pin);
            }
        }

        public async void FindMe(Map map)
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

            position = await locator.GetPositionAsync();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));
        }

    }
}
