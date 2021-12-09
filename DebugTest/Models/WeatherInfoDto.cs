using Newtonsoft.Json;
using System;

namespace DebugTest.Models
{
    public class WeatherInfoDto
    {
        [JsonProperty("consolidated_weather")]
        public ConsolidatedWeatherDto[] consolidated_weather { get; set; }

        public DateTime Time { get; set; }

        [JsonProperty("sun_rise")]
        public DateTime SunRise { get; set; }

        [JsonProperty("sun_set")]
        public DateTime SunSet { get; set; }

        [JsonProperty("timezone_name")]
        public string TimezoneName { get; set; }

        public ParentDto Parent { get; set; }

        public SourceDto[] Sources { get; set; }

        public string Title { get; set; }

        public string LocationType { get; set; }

        public int Woeid { get; set; }

        [JsonProperty("latt_long")]
        public string LattLong { get; set; }

        public string Timezone { get; set; }
    }
}
