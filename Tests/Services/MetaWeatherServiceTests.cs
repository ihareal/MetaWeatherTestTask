using DebugTest.Helpers;
using DebugTest.Models;
using DebugTest.Services;
using DebugTest.Services.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// I've wrote an example of tests just to mention that I am thinking about quality of the code base.
    /// I didn't covered all methods because it is redundant job in terms of current test task
    /// </summary>
    public class MetaWeatherServiceTests
    {
        private Mock<MetaWeatherService> _metaWeatherService;
        private Mock<IFileService> _fileService;
        private Mock<IRequestSender> _requestSender;
        private Mock<IReadResponseMessageHelper> _readResponseMessageHelper;

        [SetUp]
        public void Setup()
        {
            _fileService = new Mock<IFileService>();
            _requestSender = new Mock<IRequestSender>();
            _readResponseMessageHelper = new Mock<IReadResponseMessageHelper>();
            _metaWeatherService = new Mock<MetaWeatherService>(_requestSender.Object, _fileService.Object, _readResponseMessageHelper.Object);
            _metaWeatherService.CallBase = true;
        }

        [Test]
        public async Task SaveWeatherByLocationInfoToFileAsync_InputDataIsCorrect_WeatherInfoSuccessfullySavedToFile()
        {
            // Arrange
            int id = 1;
            string location = "lc";
            _metaWeatherService.Setup(x => x.GetLocationIdAsync(location)).ReturnsAsync(id);
            _metaWeatherService.Setup(x => x.GetWeatherInfoDtoByLocationIdAsync(It.IsAny<int>())).ReturnsAsync(It.IsAny<WeatherInfoDto>());

            // Act
            await _metaWeatherService.Object.SaveWeatherByLocationInfoToFileAsync(location);

            // Assert
            _fileService.Verify(x => x.WriteWeatherInfoIntoFile(It.IsAny<WeatherInfoDto>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SaveWeatherByLocationInfoToFileAsync_LocationIdIsNull_ThrowsException()
        {
            // Arrange
            int? nullableId = null;
            _metaWeatherService.Setup(x => x.GetLocationIdAsync(It.IsAny<string>())).ReturnsAsync(nullableId);

            // Act && Assert
            Assert.ThrowsAsync<Exception>(async() => await _metaWeatherService.Object.SaveWeatherByLocationInfoToFileAsync(It.IsAny<string>()));
            _metaWeatherService.Verify(x => x.GetWeatherInfoDtoByLocationIdAsync(It.IsAny<int>()), Times.Never);
            _fileService.Verify(x => x.WriteWeatherInfoIntoFile(It.IsAny<WeatherInfoDto>(), It.IsAny<string>()), Times.Never);
        }


        [Test]
        public async Task SaveWeatherByLocationInfoToFileAsync_LocationIsNull_ThrowsHttpRequestException()
        {
            // Arrange
            int id = 1;
            string location = string.Empty;
            _metaWeatherService.Setup(x => x.GetLocationIdAsync(location)).ThrowsAsync(new HttpRequestException());

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(async() => await _metaWeatherService.Object.SaveWeatherByLocationInfoToFileAsync(location));
        }
    }
}