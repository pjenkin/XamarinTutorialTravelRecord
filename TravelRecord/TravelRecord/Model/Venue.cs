using System;
using TravelRecord.Helpers;     // We defined Constants class in here 8-65

namespace TravelRecord.Model
{
    public class Venue
    {
        public static string GenerateURL(double latitude, double longitude)
        {
            string url = String.Format(Constants.VENUE_SEARCH, latitude, longitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));

            return url;
        }
    }
}
