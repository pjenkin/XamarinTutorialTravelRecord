using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
