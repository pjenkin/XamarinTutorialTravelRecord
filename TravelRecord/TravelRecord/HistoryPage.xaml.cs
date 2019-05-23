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

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))     // use previously established location
            // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
            // safely leave out connection.Close call as Dispose will be automatically called
            {
                conn.CreateTable<Post>();                       // create table (only) if non-existent
                var posts = conn.Table<Post>().ToList();        // get list of Post objects (ie records)

                postListView.ItemsSource = posts;               // set data context of ListView in xaml file (binding specified in xaml)

                // conn.Close();                                   // NB remembering to close connection! - not needed if 'using' SQLiteConnection
            }   // end of 'using' statement block
        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Could have referred to 'sender', but referring directly to element here
            var selectedPost = postListView.SelectedItem as Post;       // cast and check for null drekly

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetail(selectedPost));
            }
        }
    }
}