using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Geofencing.POC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Geolocator _locator;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var status = await Geolocator.RequestAccessAsync();
            if (status == GeolocationAccessStatus.Allowed)
            {
                _locator = new Geolocator();
                _locator.StatusChanged += _locator_StatusChanged;
                GeofenceMonitor.Current.StatusChanged += Current_StatusChanged;
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {GeofenceMonitor.Current.Status} ({DateTime.Now})";
                geolocationStatusTextBlock.Text = $"Geolocation Current status: {_locator.LocationStatus} ({DateTime.Now})";
            }
            else
            {
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {GeofenceMonitor.Current.Status} ({DateTime.Now})";
                geolocationStatusTextBlock.Text = $"Geolocation Current status: Denied ({DateTime.Now})";
            }
        }

        private async void _locator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                geolocationStatusTextBlock.Text = $"Geolocation Current status: {_locator.LocationStatus} ({DateTime.Now}) (status changed)";
            });
        }

        private async void Current_StatusChanged(GeofenceMonitor sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {GeofenceMonitor.Current.Status} ({DateTime.Now}) (status changed)";
            });
        }
    }
}
