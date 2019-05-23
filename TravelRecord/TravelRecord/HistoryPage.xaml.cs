using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}

        // Implementing OnAppearing so as to refresh the data on the page whenever navigated back-to
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);     // use previously established location

            conn.CreateTable<Post>();                       // create table (only) if non-existent
            var posts = conn.Table<Post>().ToList();        // get list of Post objects (ie records)

            conn.Close();                                   // NB remembering to close connection!
        }
    }
}