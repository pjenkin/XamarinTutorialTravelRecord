using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TravelRecord
{
    public partial class App : Application
    {
        public static string DatabaseLocation { get; set; } = string.Empty;

        // Azure code
        public static MobileServiceClient MobileService = new MobileServiceClient("https://travelrecord-pnj.azurewebsites.net");

        // Table interfacing between local and cloud for sync'ing 12-116
        public static IMobileServiceSyncTable<Post> postsTable;             // local table, through which sync'ing push and pull will be done           

        public static User user = new User();

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());      // default entry point changed
        }

        // Re-used from local-only for local db for offline-cloud sync'ing 12-116 - location in MainActivity(Android) or in AppDelegate(iOS) (TODO:UWP)
        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());      // default entry point changed

            DatabaseLocation = databaseLocation;                // this will be different according to platform, but this variable available to all

            var store = new MobileServiceSQLiteStore(databaseLocation);      // Re-used, declare local db for sync'ing 12-116

            store.DefineTable<Post>();                          // instantiate SQLite table (for offline/sync'ing) ready for use

            MobileService.SyncContext.InitializeAsync(store);   // initialise ready for sync'ing later

            postsTable = MobileService.GetSyncTable<Post>();      // NB Get*Sync*Table not just GetTable
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
