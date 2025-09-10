using Xunit;
using Real_time_weather_monitoring.Parsers;
using Real_time_weather_monitoring.Models;
using System;

namespace Real_time_weather_monitoring.Tests.Parsers
{
    [Trait("Category", "XmlWeatherDataParser")]
    public class XmlWeatherDataParserTests
    {
        private readonly XmlWeatherDataParser _parser = new();

        [Fact]
        public void CanParse_ValidXml_ReturnsTrue()
        {
            var input = "<WeatherData><Location>Test</Location><Temperature>25.0</Temperature><Humidity>60.0</Humidity></WeatherData>";
            Assert.True(_parser.CanParse(input));
        }

        [Fact]
        public void CanParse_InvalidXml_ReturnsFalse()
        {
            var input = "{\"key\":\"value\"}";
            Assert.False(_parser.CanParse(input));
        }

        [Fact]
        public void Parse_ValidXml_ReturnsWeatherData()
        {
            var input = "<WeatherData><Location>Test</Location><Temperature>25.0</Temperature><Humidity>60.0</Humidity></WeatherData>";
            var result = _parser.Parse(input);
            Assert.Equal("Test", result.Location);
            Assert.Equal(25.0, result.Temperature);
            Assert.Equal(60.0, result.Humidity);
        }

        [Fact]
        public void Parse_InvalidXml_ThrowsArgumentException()
        {
            var input = "<invalid></invalid>";
            Assert.Throws<ArgumentException>(() => _parser.Parse(input));
        }
    }
}
