using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using GreenMapsApp.Model;
using System.Collections.ObjectModel;
using Plugin.Geolocator;

namespace GreenMapsApp
{
    class MapHelperFunctions
    {
        // Populate map on function call
        public async void PopulateMap(Map map, Dictionary<MapLocationDatum, int> dictionary)
        {
            RestService restService = new RestService();
            string returnedJsonArray = await restService.GetAll();
            ObservableCollection<PinLocations> pinLocations = new ObservableCollection<PinLocations>();

            List<MapLocationDatum> parsedJsonArray = JsonConvert.DeserializeObject<List<MapLocationDatum>>(returnedJsonArray);

            foreach (MapLocationDatum json in parsedJsonArray)
            {
                MapLocationDatum mapLocation = new MapLocationDatum();

                Pin pin = new Pin
                {
                    Label = "Test",
                    Position = new Position(json.latitude, json.longitude),
                    Address = json.message
                };

                mapLocation.id = json.id;
                mapLocation.dateCreated = json.dateCreated;
                mapLocation.ipAddress = json.ipAddress;
                mapLocation.title = json.title;
                mapLocation.resolved = json.resolved;
                mapLocation.latitude = json.latitude;
                mapLocation.longitude = json.longitude;
                mapLocation.message = json.message;
                mapLocation.severity = json.severity;

                dictionary.Add(mapLocation, json.id);

                pin.InfoWindowClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    string pinName = ((Pin)s).Label;
                    string resolutionStatus;
                    if (mapLocation.resolved)
                    {
                        resolutionStatus = "Resolved";
                    } else
                    {
                        resolutionStatus = "Unreolved";
                    }
                    bool resolved = await App.Current.MainPage.DisplayAlert("Resolve " + pinName + "?", mapLocation.title + "\n" + mapLocation.message + "\n" + mapLocation.latitude.ToString("0.0000000000") + ":" + mapLocation.longitude.ToString("0.0000000000") + "\n" + "Severity: " + mapLocation.severity + "\n" + "Status: " + resolutionStatus, "Yes", "No");
                    if (resolved)
                    {
                        await restService.UpdateResolved(mapLocation, dictionary);
                    }
                };

                map.Pins.Add(pin);
            }
        }

        public async void PopulateMapSearch(Map map, Dictionary<MapLocationDatum, int> dictionary, string input)
        {
            RestService restService = new RestService();
            string returnedJsonArray = await restService.SearchRESTAPI(input);
            ObservableCollection<PinLocations> pinLocations = new ObservableCollection<PinLocations>();

            List<MapLocationDatum> parsedJsonArray = JsonConvert.DeserializeObject<List<MapLocationDatum>>(returnedJsonArray);

            foreach (MapLocationDatum json in parsedJsonArray)
            {
                MapLocationDatum mapLocation = new MapLocationDatum();

                Pin pin = new Pin
                {
                    Label = "Test",
                    Position = new Position(json.latitude, json.longitude),
                    Address = json.message
                };

                mapLocation.id = json.id;
                mapLocation.dateCreated = json.dateCreated;
                mapLocation.ipAddress = json.ipAddress;
                mapLocation.title = json.title;
                mapLocation.resolved = json.resolved;
                mapLocation.latitude = json.latitude;
                mapLocation.longitude = json.longitude;
                mapLocation.message = json.message;
                mapLocation.severity = json.severity;

                dictionary.Add(mapLocation, json.id);

                pin.InfoWindowClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    string pinName = ((Pin)s).Label;
                    bool resolved = await App.Current.MainPage.DisplayAlert("Resolve " + pinName, Convert.ToString(mapLocation), "Yes", "No");
                    if (resolved)
                    {
                        await restService.UpdateResolved(mapLocation, dictionary);
                    }
                };

                map.Pins.Add(pin);
            }
        }

        // Gets current location of user
        public async void FindMe(Map map)
        {
            Plugin.Geolocator.Abstractions.IGeolocator locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position;

            position = await locator.GetPositionAsync();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));
        }

    }
}
