using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TravelRecord.Model;       // use Model defined by us earlier in 6-51
using SQLite;
using Plugin.Geolocator;
using TravelRecord.Logic;
using TravelRecord.ViewModel;

namespace TravelRecord
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        //Post post;                                              // for use within NewTravelPage and for BindingContext of elements in page
        // post superseded as BindingContext in 12-105

        NewTravelVM viewModel;

        public bool AllowSaveButton = false;                     // workaround?bodge? - PNJ trying to get save Binding PostCommand button in NewTravelCommand working ok - shouldn't need this

        public NewTravelPage()
        {
            InitializeComponent();

            // post = new Post();
            //containerStackLayout.BindingContext = post;
            // post superseded as BindingContext in 12-105

            viewModel = new NewTravelVM();
            BindingContext = viewModel;
            // NB BindingContext (viewModel) now for entire page, not just for StackLayout...
            // ... - must now set Binding of StackLayout only to Post (property of ViewModel) in XAML (qv) 12-105
            AllowSaveButton = true;                          // set bodge/workaround flag for disabled (no CanExecute?) button at end of constructor - 12-105 
            
        }

        // override here - this called whenever page loaded by new user
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }
    }
}
/*
        private async void SaveToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            { 
  */              /*
                var selectedVenue = venueListView.SelectedItem as Venue;    // get clicked Venue as object of Venue class

                var firstCategory = selectedVenue.categories.FirstOrDefault();      // approved, safe, way of getting first of zero to many categories of venue

                // insert Post record into db
                // Post post = new Post()           // not needed after refactoring for MVVM & Binding in 12-99
                //{
                // ExperienceDescription = experienceDescriptionEntry.Text,
                // use new text entry box value for description (otherwise use deserialised JSON data fields from API response)
                // ExperienceDescription via BindingContext after 12-99
                // Set values using deserialised JSON data fields from API response
                // Id set automatically
                // CategoryId = selectedVenue.categories[0].id;        // just get first category - not safe way though
                post.CategoryId = firstCategory?.id;                                     // PNJ null conditional operator
                post.CategoryName = firstCategory?.name ?? "No category name given";     // PNJ null conditional & coalescing operators
                post.Address = selectedVenue.location.address;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.Distance = selectedVenue.location.distance;
                post.VenueName = selectedVenue.name;
                post.UserId = App.user.Id;                                    // set the app's current user to Azure-cloud-stored ID
                */
                // above refactored out in 12-105 for MVVM
                // };          // NB initialising the new instance's members thus in a terminated-block
                            /*
                                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                                            // Use db location class member defined earlier in 6-49
                                            // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
                                            // safely leave out connection.Close call as Dispose will be automatically called
                                            {
                                                conn.CreateTable<Post>();

                                                int numRows = conn.Insert(post);  // type can also be used automatically to deduce to which table to insert

                                                // conn.Close();       // close the connection as well - not needed if 'using' SQLiteConnection

                                                if (numRows > 0)
                                                {
                                                    // diagnostic alert
                                                    DisplayAlert("Success", "Record successfully inserted", "OK");
                                                }
                                                else
                                                {
                                                    DisplayAlert("Failure", "No record inserted", "OK");
                                                }
                                            }       // end of 'using' statement block
                            */
                            // No longer using SQLite after switching to Azure in 11-89

                // insert record to Azure db Posts table (having already gotten foreign key for ID from User table)
                //await App.MobileService.GetTable<Post>().InsertAsync(post);
/*
            Post.Insert(post);      // refactored to MVVM
            await DisplayAlert("Success", "Record successfully inserted", "OK");


            }
            catch (NullReferenceException nre)      // e.g. in case there's no category (null)
            {
                DisplayAlert("Failure", "No record inserted", "OK");
            }
            catch (Exception ex)                // for more general exceptions that specifically null exceptions
            {
                DisplayAlert("Failure", "No record inserted", "OK");
            }
        } 
    }
    
}
*/