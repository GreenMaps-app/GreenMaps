using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GreenMapsApp.ViewModel;

namespace GreenMapsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();

            var mockJsonArray = new[]
           {
                new
                {
                     date_created = "2021 - 10 - 13 05:16:56.680",
                     ip_address = "127.0.0.1",
                     title = "This is the first title",
                     latitude = 49.260279,
                     longitutde = -123.179432,
                     message = "This is the first entry"
                },
                new
                {
                     date_created = "2021 - 10 - 13 05:16:56.680",
                     ip_address = "127.0.0.1",
                     title = "This is the second title",
                     latitude = 49.270260,
                     longitutde =  -123.209653,
                     message = "This is the second entry"
                },
                new
                {
                     date_created = "2021 - 10 - 13 05:16:56.680",
                     ip_address = "127.0.0.1",
                     title = "This is the third title",
                     latitude = 49.255760,
                     longitutde = -123.227549,
                     message = "This is the third entry"
                }
            };



            // EFFECTS: On startup loads all of the data from API and places pins with appropriate comments,titles and locations
            //          Also centres map on current location

            Map map = new Map
            {
                IsShowingUser = true
            };

            Pin pin = new Pin()
            {
                Label = "Santa Cruz",
                Address = "The city with a boardwalk",
                Type = PinType.Place,
                Position = new Position(36.9628066, -122.0194722)
            };
            map.Pins.Add(pin);
            Content = map;
            // EFFECTS: On Button press prompts user the enter title description and takes users current location and places a new pin
            //          Also adds data to API (Currently not needed)

            List<PinLocations> pinLocations = new List<PinLocations>();

            foreach (var json in mockJsonArray)
            {
                string parsedJson = JsonConvert.SerializeObject(json);
                JObject parsedJsonObj = JObject.Parse(parsedJson);
                Console.WriteLine((string)parsedJsonObj["latitude"] + (string)parsedJsonObj["longitutde"]);


                PinLocations temp = new PinLocations { Latitude = (string)parsedJsonObj["latitude"], Longitutde = (string)parsedJsonObj["longitutde"] };
                pinLocations.Add(temp);
            }

            foreach (var item in pinLocations)
            {
                Pin temp = new Pin();

                temp.Label = "Test";
                temp.Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitutde));
                
                map.Pins.Add(temp);
            }

            Frame frame = new Frame
            {
                BorderColor = Color.Orange,
                CornerRadius = 10,
                HasShadow = true,
                Content = new Label { Text = "Example" }
            };

            

        }
    }
}

