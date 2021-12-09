using Newtonsoft.Json;
using System;

namespace DebugTest.Models
{
    public class ConsolidatedWeatherDto
    {
        public long Id { get; set; }

        [JsonProperty("weather_state_name")]
        public string WeatherStateName { get; set; }

        [JsonProperty("weather_state_abbr")]
        public string WeatherStateAbbr { get; set; }

        [JsonProperty("wind_direction_compass")]
        public string WindDirectionCompass { get; set; }

        public DateTime Created { get; set; }

        [JsonProperty("applicable_date")]
        public string ApplicableDate { get; set; }

        [JsonProperty("min_temp")]
        public float MinTemp { get; set; }

        [JsonProperty("max_temp")]
        public float MaxTemp { get; set; }

        [JsonProperty("the_temp")]
        public float TheTemp { get; set; }

        [JsonProperty("wind_speed")]
        public float WindSpeed { get; set; }

        [JsonProperty("wind_direction")]
        public float WindDirection { get; set; }

        [JsonProperty("air_pressure")]
        public float AirPressure { get; set; }

        public int Humidity { get; set; }

        public float Visibility { get; set; }

        public int Predictability { get; set; }

    }
}
