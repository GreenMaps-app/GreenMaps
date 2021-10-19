using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using GreenMapsApp.Model;
using System.Net;

namespace GreenMapsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();

            Dictionary<MapLocationDatum,int> dictionary = new Dictionary<MapLocationDatum,int>();
            RestService restService = new RestService();
            MapHelperFunctions mapHelper = new MapHelperFunctions();

            Map map = new Map
            {
                IsShowingUser = true,
                
            };
            
            // Centers map on user on startup
            mapHelper.FindMe(map);

            // Map on click event, adds pins
            async void OnMapClicked(object sender, MapClickedEventArgs e)
            {
                bool valid = true;
                bool answer = await DisplayAlert("Would you like to add a pin", "", "Yes", "No");
                if (answer)
                {
                    string label = await DisplayPromptAsync("Add Title","", initialValue: "", maxLength: 49, keyboard: Keyboard.Default);

                    if (label == "" || label == null)
                    {
                        valid = false;
                    }

                    string description = await DisplayPromptAsync("Add Description", "", initialValue: "", maxLength: 100, keyboard: Keyboard.Default);
                    
                    string severity = "low";
                    severity = await DisplayActionSheet("How severe is the environmental issue", "Cancel", null, "High", "Medium", "Low");
                    
                    // create MapLocationDatum to add to REST API
                    MapLocationDatum outputJson = new MapLocationDatum();
                    foreach(IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
                    {
                        outputJson.ipAddress = address.ToString();
                        break;
                    }
                    outputJson.latitude = e.Position.Latitude;
                    outputJson.longitude = e.Position.Longitude;
                    outputJson.title = label;
                    outputJson.resolved = false;
                    outputJson.message = description;
                    outputJson.dateCreated = DateTime.Now;
                    outputJson.severity = severity.ToLower();
                    if (valid)
                    {
                        Pin pin = new Pin
                        {
                            Label = label,
                            Position = new Position(e.Position.Latitude, e.Position.Longitude),
                            Address = description
                        };
                        // Adds pin information to REST API
                        string json = JsonConvert.SerializeObject(outputJson);
                        outputJson.id = await restService.Post(json);
                        // Adds MapLocationDatum to dictionary
                        dictionary.Add(outputJson, outputJson.id);
                        pin.InfoWindowClicked += async (s, args) =>
                        {
                            args.HideInfoWindow = true;
                            string pinName = ((Pin)s).Label;
                            string resolutionStatus;
                            if (outputJson.resolved)
                            {
                                resolutionStatus = "Resolved";
                            }
                            else
                            {
                                resolutionStatus = "Unresolved";
                            }
                            bool resolved = await DisplayAlert("Resolve " + pinName + "?", outputJson.title + "\n" + outputJson.message + "\n" + outputJson.latitude.ToString("0.0000000000") + ":" + outputJson.longitude.ToString("0.0000000000") + "\n" + "Severity: " + outputJson.severity + "\n" + "Status: " + resolutionStatus, "Yes", "No");
                            if (resolved)
                            {
                                await restService.UpdateResolved(outputJson, dictionary);
                            }
                        };

                        map.Pins.Add(pin);
                    } else
                    {
                        await DisplayAlert("Alert", "Must include label name", "OK");
                    }
                }
            }

            map.MapClicked += OnMapClicked;

            // Populate map with REST API information on startup
            mapHelper.PopulateMap(map, dictionary);
            Entry entry = new Entry();
            entry.BackgroundColor = Color.FromHex("#2c3e50");

            void EntryCompleted(object sender, EventArgs e)
            {
                if (!(((Entry)sender).Text == null || ((Entry)sender).Text == ""))
                {
                    map.Pins.Clear();
                    string text = ((Entry)sender).Text;
                    dictionary = new Dictionary<MapLocationDatum, int>();
                    mapHelper.PopulateMapSearch(map, dictionary, text);
                }
            }

            entry.Completed += EntryCompleted;

            StackLayout stackLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("#77d065"),
                Margin = new Thickness(0),
                Children =
                {
                    new Label { Text = "Green Maps",TextColor = Color.FromHex("#77d065"), FontSize = 20, HorizontalOptions = LayoutOptions.Center},
                    map,
                    entry
                }
            };

            Content = stackLayout;
        }
    }
}

