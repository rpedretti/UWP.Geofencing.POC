using System;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP.Geofencing.POC
{
    public sealed partial class MainPage : Page
    {
        private Geolocator _locator;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var status = await Geolocator.RequestAccessAsync();

            _locator = new Geolocator();
            _locator.StatusChanged += _locator_StatusChanged;
            GeofenceMonitor.Current.StatusChanged += Current_StatusChanged;

            if (status == GeolocationAccessStatus.Allowed)
            {
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {GeofenceMonitor.Current.Status} ({DateTime.Now})";
                geolocationStatusTextBlock.Text = $"Geolocation Current status: {_locator.LocationStatus} ({DateTime.Now})";
            }
            else
            {
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {GeofenceMonitor.Current.Status} ({DateTime.Now})";
                geolocationStatusTextBlock.Text = $"Geolocation Current status: {_locator.LocationStatus} ({DateTime.Now})";
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _locator.StatusChanged -= _locator_StatusChanged;
            GeofenceMonitor.Current.StatusChanged -= Current_StatusChanged;
            base.OnNavigatedFrom(e);
        }

        private async void _locator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                geolocationStatusTextBlock.Text = $"Geolocation Current status: {sender.LocationStatus} ({DateTime.Now}) (status changed)";
            });
        }

        private async void Current_StatusChanged(GeofenceMonitor sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                geofencingStatusTextBlock.Text = $"Geofencing Current status: {sender.Status} ({DateTime.Now}) (status changed)";
            });
        }
    }
}
