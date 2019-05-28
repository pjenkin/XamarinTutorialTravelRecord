using Microsoft.WindowsAzure.MobileServices;
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

        public static User user = new User();

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());      // default entry point changed
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());      // default entry point changed

            DatabaseLocation = databaseLocation;                // this will be different according to platform, but this variable available to all

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
