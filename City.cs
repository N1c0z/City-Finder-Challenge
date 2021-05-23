using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace City_Finder_Challenge
{
    class City
    {
        [JsonPropertyName("post code")]
        public string Postcode { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }
        public List<Place> Places { get; set; }
    }

}
