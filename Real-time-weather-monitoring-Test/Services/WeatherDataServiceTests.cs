using Xunit;
using Real_time_weather_monitoring.Services;
using Real_time_weather_monitoring.Bots;
using Real_time_weather_monitoring.Models;
using Moq;
using System.Collections.Generic;
using System;

namespace Real_time_weather_monitoring.Tests.Services
{
    [Trait("Category", "WeatherDataService")]
    public class WeatherDataServiceTests
    {
        private readonly Mock<IWeatherDataParser> _mockParser;
        private readonly List<IWeatherBot> _bots;
        private readonly WeatherDataService _service;

        public WeatherDataServiceTests()
        {
            _mockParser = new Mock<IWeatherDataParser>();
            _bots = new List<IWeatherBot>();
            _service = new WeatherDataService(_mockParser.Object, _bots);
        }

        [Fact]
        public void ParseWeatherData_CallsParseOnParser()
        {
            var input = "test";
            var expected = new WeatherData();
            _mockParser.Setup(p => p.Parse(input)).Returns(expected);
            var result = _service.ParseWeatherData(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProcessWeatherData_CallsUpdateOnAllBots()
        {
            var bot1 = new Mock<IWeatherBot>();
            var bot2 = new Mock<IWeatherBot>();
            _bots.Add(bot1.Object);
            _bots.Add(bot2.Object);
            var data = new WeatherData();
            _service.ProcessWeatherData(data);
            bot1.Verify(b => b.Update(data), Times.Once);
            bot2.Verify(b => b.Update(data), Times.Once);
        }

        [Fact]
        public void ProcessInput_ValidInput_ProcessesData()
        {
            var input = "test";
            var data = new WeatherData();
            _mockParser.Setup(p => p.Parse(input)).Returns(data);
            _service.ProcessInput(input);
            _mockParser.Verify(p => p.Parse(input), Times.Once);
        }

        [Fact]
        public void ProcessInput_ParseThrowsException_HandlesException()
        {
            var input = "test";
            _mockParser.Setup(p => p.Parse(input)).Throws(new Exception("Parse error"));
            using var sw = new System.IO.StringWriter();
            Console.SetOut(sw);
            _service.ProcessInput(input);
            var output = sw.ToString();
            Assert.Contains("Error: Parse error", output);
        }
    }
}
