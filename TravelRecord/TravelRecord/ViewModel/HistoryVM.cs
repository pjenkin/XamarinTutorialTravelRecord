using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        internal async void UpdatePosts()
        {
            // throw new NotImplementedException();
            //var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            // Posts.Clear();                // elements may already exist in list - clear list before fresh fetch from db - causing duplication however

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

        }
    }
}
