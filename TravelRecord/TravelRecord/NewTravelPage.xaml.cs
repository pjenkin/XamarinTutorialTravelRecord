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

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
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

        private void SaveToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            { 
                var selectedVenue = venueListView.SelectedItem as Venue;    // get clicked Venue as object of Venue class

                var firstCategory = selectedVenue.categories.FirstOrDefault();      // approved, safe, way of getting first of zero to many categories of venue

                // insert Post record into db
                Post post = new Post()
                {
                    ExperienceDescription = experienceDescriptionEntry.Text,
                    // use new text entry box value for description (otherwise use deserialised JSON data fields from API response)
                    // Id set automatically
                    // CategoryId = selectedVenue.categories[0].id;        // just get first category - not safe way though
                    CategoryId = firstCategory.id,
                    CategoryName = selectedVenue.location.address,
                    Address = selectedVenue.location.address,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    Distance = selectedVenue.location.distance,
                    VenueName = selectedVenue.name

                };          // NB initialising the new instance's members thus in a terminated-block

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
            }
            catch (NullReferenceException nre)      // e.g. in case there's no category (null)
            {

            }
            catch (Exception ex)                // for more general exceptions that specifically null exceptions
            {

            }
        } 
    }
}