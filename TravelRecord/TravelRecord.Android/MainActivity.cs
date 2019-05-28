using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Plugin.Permissions;
using Microsoft.WindowsAzure.MobileServices;

namespace TravelRecord.Droid
{
    [Activity(Label = "TravelRecord", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);       // Initialise maps, specifying activity & bundle - AndroidManifest.xml must be edited 
                                                                    // LoadApplication(new App());              // boilerplate instantiation of 
            // Azure code 11-86
            CurrentPlatform.Init();

            // use Plugin.Permissions (step 2 of 3 for Plugin.Permissions permissions plugin)
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);// Initialise permissions, specifying activity & bundle - AndroidManifest.xml must be edited 
            // step 3 of 3 for Plugin.Permissions - in Info.plist, add 'Privacy-Calendars Usage', 'Privacy - Bluetooth Peripheral Usage Description'


            // define the location of the db in terms of path
            string dbName = "travel_db.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = System.IO.Path.Combine(folderPath, dbName);

            LoadApplication(new App(fullPath));

          
        }

        // use Plugin.Permissions (step 1 of 3 for Plugin.Permissions permissions plugin)
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}