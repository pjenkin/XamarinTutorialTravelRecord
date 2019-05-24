using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
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
    }
}