using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GreenMapsApp
{
    public class PageBase : ContentPage
    {
        public void DisplayAlertHelper(string pinName)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
            });
        }

    }
}
