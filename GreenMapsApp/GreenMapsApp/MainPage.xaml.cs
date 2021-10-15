using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GreenMapsApp.ViewModel;

namespace GreenMapsApp
{
    public partial class MainPage : ContentPage
    {
        MapPageViewModel mapPageViewModel;
        public MainPage()
        {

            InitializeComponent();

            // EFFECTS: On startup loads all of the data from API and places pins with appropriate comments,titles and locations
            //          Also centres map on current location

            BindingContext = mapPageViewModel = new MapPageViewModel();

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

            // EFFECTS: On Button press prompts user the enter title description and takes users current location and places a new pin
            //          Also adds data to API (Currently not needed)

            void buttonAddPin()
            {

            }

            



        }
    }
}

