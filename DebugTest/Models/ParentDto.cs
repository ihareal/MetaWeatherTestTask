using Newtonsoft.Json;

namespace DebugTest.Models
{
    public class ParentDto
    {        
        public string title { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        public int Woeid { get; set; }

        [JsonProperty("latt_long")]
        public string LattLong { get; set; }
    }
}
