using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;

namespace TravelRecord.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        /// <summary>
        /// Ensure ViewModel's Posts member has an accurate list of posts, as in database
        /// </summary>
        //internal async void UpdatePosts()
        //public async void UpdatePosts()
        public async Task<bool>UpdatePosts()
        // had to make async so Task as to ensure would be awaited so isRefreshing flag not cleared too early; an Tasks must return something (not void)
        {
            // throw new NotImplementedException();
            //var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            // Posts.Clear();                // elements may already exist in list - clear list before fresh fetch from db - causing duplication however
            try
            {
                var posts = await Post.Read();          // Read posts for current user in App - refactored 12-95 for MVVM
                if (posts != null)                      // to avoid duplication due to race hazard, only populate list if blank list at present
                {
                    Posts.Clear();                      // elements may already exist in list - clear list before fresh fetch from db
                    foreach (var post in posts)
                    {
                        Posts.Add(post);          // build up code-behind list of posts
                    }
                }
                // Itemsource of ListView for ObervableList set in XAML
                //postListView.ItemsSource = posts;     // REMmed out in 12-110

                return true;
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error",e.Message,"Ok");
                return false;
            }
        }

        /// <summary>
        /// Delete a specified travel 'post' record
        /// </summary>
        /// <param name="post"></param>
        public async void DeletePost(Post post)
        {
            await Post.Delete(post);
        }
    }
}
