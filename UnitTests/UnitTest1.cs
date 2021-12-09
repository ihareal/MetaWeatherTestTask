using DebugTest.Services;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        private Mock<MetaWeatherService> _metaWeatherServiceMock;

        [SetUp]
        public void Setup()
        {
            _metaWeatherServiceMock = new Mock<MetaWeatherService>();
        }

        [Test]
        public void SaveWeatherByLocationInfoToFileAsync_()
        {
            
        }
    }
}