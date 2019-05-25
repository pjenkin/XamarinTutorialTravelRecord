using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecord.Helpers
{
    public class Constants
    {
        public const string VENUE_SEARCH = "https://api.foursquare.com/v2/venues/search?ll={0},{1}&client_id={2}&client_secret={3}&v={4}";

        // https://api.foursquare.com/v2/venues/search?ll=40.7,-74&client_id=CLIENT_ID&client_secret=CLIENT_SECRET&v=YYYYMMDD from https://developer.foursquare.com/docs/api/configuration/authentication
        // Use String.Format presently 8-65

        public const string CLIENT_ID = "G1JNW5Z5HBYZTY1OKGP2CKQMMA12I1ISBFRUGKANPBMRDJTI";     // ought not to be uploading these to online public repo, but hey nvmd
        public const string CLIENT_SECRET = "3DKYDIHFHVJGM3PNBMPPUDCNKU3LPL0BFKSLB3FZRBGCNYAA";
        // From FourSquare API developer console, having used 'Create App' (in 8-64)
        
    }
}
