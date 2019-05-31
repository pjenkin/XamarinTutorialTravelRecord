using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;
using TravelRecord.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{

        HistoryVM viewModel;

		public HistoryPage ()
		{
			InitializeComponent ();

            viewModel = new HistoryVM();
            BindingContext = viewModel;
		}

        // Implementing OnAppearing so as to refresh the data on the page whenever navigated back-to
        //protected  override async void OnAppearing()
        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*
                        using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))     // use previously established location
                                                                                                       // Since SQLiteConnection (qv) is implementing IDisposable, we can, with a 'using' statement, 
                                                                                                       // safely leave out connection.Close call as Dispose will be automatically called

                        {
                            conn.CreateTable<Post>();                       // create table (only) if non-existent
                            var posts = conn.Table<Post>().ToList();        // get list of Post objects (ie records)

                            postListView.ItemsSource = posts;               // set data context of ListView in xaml file (binding specified in xaml)

                            // conn.Close();                                   // NB remembering to close connection! - not needed if 'using' SQLiteConnection
                        }   // end of 'using' statement block
            */
            // From 11-89 onwards, reading from Azure database not from local SQLite

            /*
                        //var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

                        viewModel.Posts.Clear();                // elements may already exist in list - clear list before fresh fetch from db

                        var posts = await Post.Read();          // Read posts for current user in App - refactored 12-95 for MVVM
                        foreach (var post in posts)
                        {
                            viewModel.Posts.Add(post);          // build up code-behind list of posts
                        }
                        // Itemsource of ListView for ObervableList set in XAML
                        //postListView.ItemsSource = posts;     // REMmed out in 12-110
            */
            viewModel.UpdatePosts();                            // update the list of posts in the view NB Ctrl. 12-110
        }

        // still not quite MVVM - TODO add this to HistoryVM?? ??

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Could have referred to 'sender', but referring directly to element here
            var selectedPost = postListView.SelectedItem as Post;       // cast and check for null drekly

            if (selectedPost != null)
            {
                Navigation.PushAsync(new PostDetail(selectedPost));
            }
        }

        // TODO could convert this to a Command for VM to make MVVM 12-115
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((MenuItem)sender).CommandParameter;     // NB in XAML MenuItem, CommandParameter="{Binding}" - NB also chained casting
            viewModel.DeletePost(post);

            await viewModel.UpdatePosts();                                    // update ViewModel's list of posts after this deletion
        }

        // pull down to refresh - TODO implement as MVVM using RefreshCommand instead of Refreshing event in XAML
        private async void PostListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdatePosts();              // update the (historical) list of posts - need to make this async Task
            postListView.IsRefreshing = false;          // clear flag to show refreshing is now over (need to have an async Task above)
        }
    }
}