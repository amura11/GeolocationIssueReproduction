using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoLocationNotChanging
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            _timer = new Timer(2000);
            _timer.Elapsed += HandleTimerTick;
            _timer.Start();
        }

        private async void HandleTimerTick(object sender, ElapsedEventArgs e)
        {
            if (MainThread.IsMainThread == false)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2));
                    Location currentLocation = await Geolocation.GetLocationAsync(request);

                    Console.WriteLine($"Location: {currentLocation.Latitude}, {currentLocation.Longitude}");
                });
            }

            //We don't want requests to get the location backing up so we manually reset the time here instead of relying on the AutoReset property
            _timer.Start();
        }

        private readonly Timer _timer;
    }
}
