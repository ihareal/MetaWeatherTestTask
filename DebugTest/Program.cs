using DebugTest.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DebugTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
             .AddServices()
             .BuildServiceProvider();

            await ProcessUserActionAsync(serviceProvider.GetService<IMetaWeatherService>());
        }

        static async Task ProcessUserActionAsync(IMetaWeatherService metaWeatherService)
        {
            bool isContinueToSaveInfo = true;
            do
            {
                Console.WriteLine("Please, write \"lc\" if you want to get weather info by location.");
                Console.WriteLine("Please, write \"lt\" if you want to get weather info by latitude and longitude.");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "lc":
                        Console.WriteLine("Please, write the location:");
                        var location = Console.ReadLine();
                        await metaWeatherService.SaveWeatherByLocationInfoToFileAsync(location);
                        break;
                    case "lt":
                        Console.WriteLine("Please, enter the latitude: ");
                        var latitude = double.Parse(Console.ReadLine());
                        Console.WriteLine("Please, enter the longitude: ");
                        var longitude = double.Parse(Console.ReadLine());

                        await metaWeatherService.SaveWeatherInfoByLatitudeAndLongitudeToFileAsync(latitude, longitude);
                        break;
                    default:
                        Console.WriteLine("You've wrote an unexisting variant!");
                        break;
                }

                Console.WriteLine("Do you want to proceed(y/n)?");
                string userAnswer = Console.ReadLine();
                isContinueToSaveInfo = userAnswer == "y" ? true : false;
                Console.WriteLine();
            } while (isContinueToSaveInfo);
        }
    }

}
