using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecord.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        //public int Id { get; set; }                 // might need to be GUID to match Azure ID type 
        //public Guid Id { get; set; }                 // might need to be GUID to match Azure ID type 
        public string Id { get; set; }                 // might need to be GUID to match Azure ID type 


        [MaxLength(250)]
        public string ExperienceDescription { get; set; }


        public string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        //public Guid UserId { get; set; }                // read from Azure db table
        public string UserId { get; set; }                // read from Azure db table
        // Be very cautious using Guids viz strings - Guids can mess things up (perhaps with Azure lambda expressions for sure)

        // I'm not sure why we don't just have a Venue ID, CategoryID &c in here, relational-style (TODO??)

        /// <summary>
        /// Insert a post to the db
        /// Refactored here for MVVM
        /// Maybe DI should be used so that insertion could be to local or to cloud (TODO)
        /// </summary>
        /// <param name="post"></param>
        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);         // insert post - to Azure cloud db
        }

        /// <summary>
        /// Read list of posts (for a user recorded in the App object)
        /// 
        /// Refactored here for MVVM
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Post>> Read()
        {
            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();     // get posts filtered by user - from Azure cloud db
            return posts;
        }

        /// <summary>
        /// Return distinct dictionary of categories posted and number of times for each
        /// TODO Check - not by user?
        /// Refactored for MVVM
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            var categories = (from p in posts
                              orderby p?.CategoryId
                              select p?.CategoryName ?? "No category given").Distinct().ToList();
            // Don't show duplicated category name values (null conditional and coalescing added by PNJ)

            // Alternative syntax (arrow/anonymous/lambda syntax) - NB 'select' in LINQ to get 'CategoryName' string not whole record
            var categoriesLambda = posts.OrderBy(p => p?.CategoryId).Select(p => p?.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();        // make key/value dictionary of tally counts of categories

            foreach (var category in categories)
            {
                var count = (from post in posts
                             where post.CategoryName == category
                             select post).ToList().Count;               // LINQ used to count

                categoriesCount.Add(category, count);                   // add dictionary entry
            }

            return categoriesCount;
        }
    }
}
