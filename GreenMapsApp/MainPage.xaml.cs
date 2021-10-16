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

            RestService restService = new RestService();
            var JsonArray = restService.Get("https://greenmapsapi.azurewebsites.net/api/MapLocation/1");
            MapHelperFunctions mapHelper = new MapHelperFunctions();



            // EFFECTS: On startup loads all of the data from API and places pins with appropriate comments,titles and locations
            //          Also centres map on current location

            Map map = new Map
            {
                IsShowingUser = true
            };
            mapHelper.PopulateMap(map);
            Content = map;
           

            Frame frame = new Frame
            {
                BorderColor = Color.Orange,
                CornerRadius = 10,
                HasShadow = true,
                Content = new Label { Text = "Example" }
            };

            mapHelper.PopulateMap(map);




        }
    }
}

