using DebugTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebugTest.Services.Abstract
{
    public interface IMetaWeatherService
    {        
        Task SaveWeatherByLocationInfoToFileAsync(string location);
        
        Task<int?> GetLocationIdAsync(string location);

        Task<WeatherInfoDto> GetWeatherInfoDtoByLocationIdAsync(int id);

        Task SaveWeatherInfoByLatitudeAndLongitudeToFileAsync(double latitude, double longitude);

        Task<List<WeatherInfoByLatitudeAndLongitudeDto>> GetWeatherInfoByLatitudeAndLongitideAsync(double latitude, double longitude);
    }
}
