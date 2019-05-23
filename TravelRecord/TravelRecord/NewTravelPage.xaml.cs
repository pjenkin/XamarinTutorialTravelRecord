using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TravelRecord.Model;       // use Model defined by us earlier in 6-51
using SQLite;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        private void SaveToolbarItem_Clicked(object sender, EventArgs e)
        {
            // insert Post record into db
            Post post = new Post()
            {
                ExperienceDescription = experienceDescriptionEntry.Text
                // Id set automatically
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
    }
}