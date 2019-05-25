using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.Maps;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using TravelRecord.Model;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        private bool hasLocationPermission = false;

		public MapPage ()
		{
			InitializeComponent ();

            GetPermissions();               // use our highfalutin chinwaggin permissions-laden method below (7-62)
		}


        // all of this just to use a Plugin.Permissions so as to get around some Android 6+ problem?? 7-62
        // NB usings required: using Plugin.Permissions;using Plugin.Permissions.Abstractions;
        // step to handle permissions
        private async void GetPermissions()     // not sure if this is right (await/async?) aha! NB x await below
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                // NB different effect of Plugin.Permissions.Abstractions.Permission with different .Location... according to platform iOS or Android

                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                        status = results[Permission.LocationWhenInUse];     // could put this in a block?

                }
                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;           // ready to get user's location
                    locationsMap.IsShowingUser = true;      // if all clear (permissions) show user on map (should be error-safe)
                }
                else
                {
                    await DisplayAlert("Need your location", "We need to access your location", "Ok");
                }

                    // locationsMap.IsShowingUser = true;      // added by PNJS
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "Ok");
            }
        }
        /* what a load of *******s 7-62 */

        protected async override void  OnAppearing()                 // had to be made async as await for StartListeningAsync added (7-63)
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;     // NB pressed TAB after '+=' to get event handler in auto-complete
                // Add event handler to PositionChanged event (using += operator)
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
                // Minimum time (n/a), minimum distance (100 metres) for change - could include heading and other settings
                // By default, to conserve battery, device won't be listening for location changes - make un listen here
            }

            GetLocation();          // call bespoke geolocating method

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))     // use previously established location
            // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
            // safely leave out connection.Close call as Dispose will be automatically called
            {
                conn.CreateTable<Post>();                       // create table (only) if non-existent
                var posts = conn.Table<Post>().ToList();        // get list of Post objects (ie records)

                DisplayInMap(posts);                            // NB having typed a call, in VS CTRL+. will generate a signature
            }   // end of 'using' statement block
        }

       

        // besppoke method to loop through all posts 
        private void DisplayInMap(List<Post> posts)
        {
            // throw new NotImplementedException();             // boilerplate
            foreach(var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);      // shift map Position to record's lat/lng

                    var pin = new Xamarin.Forms.Maps.Pin()
                    // could have used namespace but this d'illustrate where stuff's from
                    {                                         // initialise the map pin's details / parameters
                        Type = PinType.SavedPin,            // type could be SearchResult, Generic &c
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                {

                }
                catch (Exception e)
                {

                }
            }

        }

        protected async override void OnDisappearing()            // 7-63 adjusted in 8-72
        {
            base.OnDisappearing();

            await CrossGeolocator.Current.StopListeningAsync();                           // turn off location detection to save battery
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;     // unsubscribe from event handler added in OnAppearing (NB using '-=' operator)
        }

        // auto-complete-added event handler method in the event of a change in the user's,location
        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            // throw new NotImplementedException();     // boilerplate

            MoveMap(e.Position);             // auto-completed signature parameter for event handler will already contain position arguments
        }

        // Bespoke method to set properties with location data using Geolocator plugin
        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();          // get user's location (if permission granted)

                MoveMap(position);              // update map display if location received
            }
        }

        // bespoke method for moving map (refactored here) 7-63
        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var centre = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(centre, 1, 1);        // 1 degree of lat & lng is the region to span around the centre

            locationsMap.MoveToRegion(span);
            // For region, supply region centre, and degrees of lat & lng to show as a region around/spanning centre
        }
    }
}