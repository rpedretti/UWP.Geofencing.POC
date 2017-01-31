# UWP.Geofencing.POC
Repo to reproduce a bug found at the Geofencing API on Windwows 10 Mobile.

This POC consists on a single page listening to both geofencing and geolocation StatusChanged to update the UI. To reproduce the errors just follow these steps:

## Actual
1. Run the app and allow location
2. Verify that both geolocation and geofencing are enabled
3. Go to location settings and revoke location permission (either all or only for the app)
4. Go back to the app and verify that geolocation is disabled and geofencing is enabled
5. Reboot the phone
6. Open the app and verify that both geolocation and geofencing are disabled
7. Go back to location settings and aloow location
8. Go back to the app and verify that geolocation is enabled and geofencing is disabled
9. Reboot the phone
10. Open the app and verify that both geolocation and geofencing are enabled

## Expected (as in desktop)
1. Run the app and allow location
2. Verify that both geolocation and geofencing are enabled
3. Go to location settings and revoke location permission (either all or only for the app)
4. Go back to the app and verify that both geolocation and geofencing are disabled
5. Go back to location settings and aloow location
6. Go back to the app and verify that both geolocation and geofencing are enabled
