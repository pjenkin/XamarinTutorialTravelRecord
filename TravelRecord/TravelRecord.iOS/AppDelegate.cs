﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace TravelRecord.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsMaps.Init();        // initialise map - also need to modeify  in Info.plist

            // Azure code 11-86
            CurrentPlatform.Init();

            // LoadApplication(new App());      // old boilerplate instantiation of App for iOS (see newer, with db parameter below)

            // define the location of the db in terms of path
            string dbName = "travel_db.sqlite";
            string folderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library");    
            string fullPath = System.IO.Path.Combine(folderPath, dbName);
            // NB don't use Personal folder, for iOS (no save there allowed), instead use Library of Personal's parent's directory 

            LoadApplication(new App(fullPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
