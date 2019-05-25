using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;               // need this to get to our Venue class

namespace TravelRecord.Logic
{
    public class VenueLogic
    {
        public async static Task <List<Venue>> GetVenues(double latitude, double longitude)      // return a list of Venues, for a given location
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
}
