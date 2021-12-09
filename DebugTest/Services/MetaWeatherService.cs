using DebugTest.Models;
using DebugTest.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DebugTest.Helpers;

namespace DebugTest.Services
{
    public class MetaWeatherService : IMetaWeatherService
    {
        private const string _weatherDomain = "https://www.metaweather.com";

        private readonly IRequestSender _requestSender;
        private readonly IFileService _fileService;
        private readonly IReadResponseMessageHelper _readResponseMessageHelper;

        public MetaWeatherService(IRequestSender requestSender,
            IFileService fileService,
            IReadResponseMessageHelper readResponseMessageHelper)
        {
            _requestSender = requestSender;
            _fileService = fileService;
            _readResponseMessageHelper = readResponseMessageHelper;
        }

        public virtual async Task SaveWeatherByLocationInfoToFileAsync(string location)
        {
            var id = await GetLocationIdAsync(location);

            if(id == null)
            {
                throw new Exception("There is no id for such location!");
            }

            var weatherInfoByLocationDto = await GetWeatherInfoDtoByLocationIdAsync(id.Value);

            var fileName = "weather" + location + DateTimeOffset.Now.ToUnixTimeSeconds() + ".json";

            Console.WriteLine($"File name: {fileName}");
            _fileService.WriteWeatherInfoIntoFile(weatherInfoByLocationDto, fileName);
        }

        public async Task SaveWeatherInfoByLatitudeAndLongitudeToFileAsync(double latitude, double longitude)
        {
            var weatherInfoByLatitudeAndLongitudeDto = await GetWeatherInfoByLatitudeAndLongitideAsync(latitude, longitude);

            var fileName = $"weather_by_latitude_and_longitude_{latitude}_{longitude}" + DateTimeOffset.Now.ToUnixTimeSeconds() + ".json";

            Console.WriteLine($"File name: {fileName}");
            _fileService.WriteWeatherInfoIntoFile(weatherInfoByLatitudeAndLongitudeDto, fileName);
        }

        public async Task<List<WeatherInfoByLatitudeAndLongitudeDto>> GetWeatherInfoByLatitudeAndLongitideAsync(double latitude, double longitude)
        {
            var response = await _requestSender.GetAsync($"{_weatherDomain}/api/location/search/?lattlong={latitude},{longitude}");

            response.EnsureSuccessStatusCode();

            using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var content = streamReader.ReadToEnd();

                return JsonConvert.DeserializeObject<List<WeatherInfoByLatitudeAndLongitudeDto>>(content);
            }
        }

        public virtual async Task<int?> GetLocationIdAsync(string location)
        {
            var response = await _requestSender.GetAsync($"{_weatherDomain}/api/location/search/?query=" + location);

            response.EnsureSuccessStatusCode();

            using (var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var content = streamReader.ReadToEnd();

                return JsonConvert.DeserializeObject<List<ParentDto>>(content)[0]?.Woeid;
            }
        }

        public virtual async Task<WeatherInfoDto> GetWeatherInfoDtoByLocationIdAsync(int id)
        {
            var response = await _requestSender.GetAsync($"{_weatherDomain}/api/location/" + id);

            response.EnsureSuccessStatusCode();

            return await _readResponseMessageHelper.ReadContentAsync<WeatherInfoDto>(response);
        }        
    }
}
