using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecord.Model
{
    public class Post : INotifyPropertyChanged          // NB 12-98 INotifyPropertyChanged (CTRL+./ALT+ENTER to implement interface)
    {
        /*
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

        */
        // Refactored in 12-95 to alter setters for use with INotifyPropertyChanged
        // propfull TAB TAB to snippet a full property definition (viz prop TAB TAB for syntactic sugar version) - NB set method defined boilerplate
        // NB Upper Case for public prop, lower case for private prop
        
        // NB Full declaring properties, so as to be able to include firing an OnPropertyChanged event in 'set' method, for Binding and INotifyPropertyChanged

        private string id;      // 'backing field' - private

        public string Id
        {
            get { return id; }
            set {
                    id = value;
                    OnPropertyChanged("Id");        // manually defined OnPropertyChanged for each property required - fire this event
                }
        }

        private string experienceDescription;

        public string ExperienceDescription
        {
            get { return experienceDescription; }
            set {
                experienceDescription = value;
                OnPropertyChanged("ExperienceDescription");
            }
        }

        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set {
                venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string categoryId;      // NB private & Public for each variable (with setter modified by hand)

        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private int distance;

        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private string userId;

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        // for 12-105, add a Venue property to make selectedVenue handling from a Command/ViewModel easier

        private Venue venue;

        [JsonIgnore ]           // from NewtonSoft.Json - to avoid error in Azure as doesn't like objects 
        public Venue Venue
        {
            get { return venue; }
            set {
                venue = value;
                // NB the order here
                var firstCategory = venue.categories?.FirstOrDefault();      // approved, safe, way of getting first of zero to many categories of venue

                // insert Post record into db
                // Post post = new Post()           // not needed after refactoring for MVVM & Binding in 12-99
                //{
                // ExperienceDescription = experienceDescriptionEntry.Text,
                // use new text entry box value for description (otherwise use deserialised JSON data fields from API response)
                // ExperienceDescription via BindingContext after 12-99
                // Set values using deserialised JSON data fields from API response
                // Id set automatically
                // CategoryId = venue.categories[0].id;        // just get first category - not safe way though
                CategoryId = firstCategory?.id;                                     // PNJ null conditional operator
                CategoryName = firstCategory?.name ?? "No category name given";     // PNJ null conditional & coalescing operators
                if (venue.location != null)
                {
                    Address = venue.location.address;
                    Latitude = venue.location.lat;
                    Longitude = venue.location.lng;
                    Distance = venue.location.distance;
                }
                    VenueName = venue?.name;
                    UserId = App.user.Id;                                    // set the app's current user to Azure-cloud-stored ID
                
                // NB order here in set method - OnPropertyChanged *last*

                OnPropertyChanged("Venue");        // hand enter this as per normal 
            }
        }


        // to correspond to CREATEDAT field in Azure db table 12-108
        private DateTimeOffset dateTimeOffset;

        public DateTimeOffset CREATEDAT
        {
            get { return dateTimeOffset; }
            set
            {
                dateTimeOffset = value;
                OnPropertyChanged("CREATEDAT");    // fire off a good old onpropertychanged event
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;       // this bit auto-completed from CTRL+. - use OnPropertyChanged (qv) to affect 'set' methods

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

        /// <summary>
        /// Handler for Propertychanged
        /// Hand-written
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)         // added by hand to work with altered 'set' methods, for INotifyPropertyChanged
        {
            if (PropertyChanged != null)                            // to avoid exception if no listeners
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  // NB INotifyPropertyChanged implementation
                                                                                    // 'this' is the 'sender' parameter - propertyName from OnPropertyChanged value hand-added to set method of full property
            }
        }
    }
}
