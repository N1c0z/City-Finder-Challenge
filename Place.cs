using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace City_Finder_Challenge
{
    public class Place
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }
        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; }
        public string Latitude { get; set; }
    }
}
