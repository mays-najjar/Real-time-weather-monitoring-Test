using Xunit;
using Real_time_weather_monitoring.Services;
using Real_time_weather_monitoring.Parsers;
using Real_time_weather_monitoring.Models;
using Moq;
using System;

namespace Real_time_weather_monitoring.Tests.Services
{
    [Trait("Category", "WeatherDataParser")]
    public class WeatherDataParserTests
    {
        private readonly WeatherDataParser _parser;
        private readonly Mock<IWeatherDataParser> _mockParser;

        public WeatherDataParserTests()
        {
            _parser = new WeatherDataParser();
            _mockParser = new Mock<IWeatherDataParser>();
        }

        [Fact]
        public void SetParser_ValidParser_SetsParser()
        {
            var exception = Record.Exception(() => _parser.SetParser(_mockParser.Object));

            Assert.Null(exception);
        }

        [Fact]
        public void SetParser_NullParser_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.SetParser(null));
        }

        [Fact]
        public void Parse_ParserSet_CallsParseOnCurrentParser()
        {
            var input = "test";
            var expected = new WeatherData { Location = "Test" };
            _mockParser.Setup(p => p.Parse(input)).Returns(expected);
            _parser.SetParser(_mockParser.Object);
            var result = _parser.Parse(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Parse_NoParserSet_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _parser.Parse("test"));
        }

        [Fact]
        public void CanParse_ParserSet_CallsCanParseOnCurrentParser()
        {
            var input = "test";
            _mockParser.Setup(p => p.CanParse(input)).Returns(true);
            _parser.SetParser(_mockParser.Object);
            var result = _parser.CanParse(input);
            Assert.True(result);
        }

        [Fact]
        public void CanParse_NoParserSet_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _parser.CanParse("test"));
        }
    }
}
