using Xunit;
using Real_time_weather_monitoring.Parsers;
using Real_time_weather_monitoring.Models;
using System;

namespace Real_time_weather_monitoring.Tests.Parsers
{
    [Trait("Category", "JsonWeatherDataParser")]
    public class JsonWeatherDataParserTests
    {
        private readonly JsonWeatherDataParser _parser = new();

        [Fact]
        public void CanParse_ValidJson_ReturnsTrue()
        {
            var json = "{\"Location\":\"Test\",\"Temperature\":25.0,\"Humidity\":60.0}";
            Assert.True(_parser.CanParse(json));
        }

        [Fact]
        public void CanParse_InvalidJson_ReturnsFalse()
        {
            var json = "<xml></xml>";
            Assert.False(_parser.CanParse(json));
        }

        [Fact]
        public void Parse_ValidJson_ReturnsWeatherData()
        {
            var json = "{\"Location\":\"Test\",\"Temperature\":25.0,\"Humidity\":60.0}";
            var result = _parser.Parse(json);
            Assert.Equal("Test", result.Location);
            Assert.Equal(25.0, result.Temperature);
            Assert.Equal(60.0, result.Humidity);
        }

        [Fact]
        public void Parse_InvalidJson_ThrowsArgumentException()
        {
            var json = "invalid json";
            Assert.Throws<ArgumentException>(() => _parser.Parse(json));
        }
    }
}
