using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TravelRecord.Helpers;     // We defined Constants class in here 8-65

namespace TravelRecord.Model
{
    public class VenueRoot
    {
        public Meta meta { get; set; }                  // this member pasted in from Example class - will contain deserialised response data
        public Response response { get; set; }          // this member pasted in from Example - will contain deserialised response data

        public static string GenerateURL(double latitude, double longitude)
        {
            string url = String.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));

            return url;
        }
    }
    // All classes below pasted from http://jsonutils.com following processing of JSON data from FourSqure API Search endpoint

    
    public class Meta
    {
        public int code { get; set; }
        public string requestId { get; set; }
    }
    // Used inside VenueRoot
    
    public class LabeledLatLng
    {
        public string label { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public IList<LabeledLatLng> labeledLatLngs { get; set; }
        public int distance { get; set; }
        public string cc { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string crossStreet { get; set; }
    }

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public Icon icon { get; set; }
        public bool primary { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }
        public string referralId { get; set; }
        public bool hasPerk { get; set; }

        /// <summary>
        /// Get list of nearby venues from FourSquare search API
        /// Refactored inside Venue class for MVVM
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async static Task<List<Venue>> GetVenues(double latitude, double longitude)      // return a list of Venues, for a given location
        {       // had to be static as 'await' used below, and had to be Task<> as returning non-void in an async
            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                // Use NewtonSoft JSON plugin to deserialize await'd json response

                venues = venueRoot.response.venues as List<Venue>;      // here will be a list of Venue objects
            }

            return venues;
        }

    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
        /*  public bool confident { get; set; } */ // not needed
    }
    /*
    public class Example
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }
    // not needed - this is an example from JsonUtil of how to use pasted classes - properties meta & response pasted inside Venue root
    */

}
