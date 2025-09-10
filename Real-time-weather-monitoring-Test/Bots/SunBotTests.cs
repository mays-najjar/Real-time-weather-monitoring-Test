using Xunit;
using Real_time_weather_monitoring.Bots;
using Real_time_weather_monitoring.Models;
using System.IO;
using System;

namespace Real_time_weather_monitoring.Tests.Bots
{
    [Trait("Category", "SnowBot")]
    public class SunBotTests
    {
        [Fact]
        public void Update_EnabledAndTemperatureAboveThreshold_PrintsActivation()
        {
            var config = new BotConfig { Enabled = true, TemperatureThreshold = 30.0, Message = "Sun Alert" };
            var bot = new SunBot(config);
            var data = new WeatherData { Temperature = 35.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.Contains("SunBot activated!", output);
            Assert.Contains("Sun Alert", output);
        }

        [Fact]
        public void Update_EnabledButTemperatureBelowThreshold_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = true, TemperatureThreshold = 30.0, Message = "Sun Alert" };
            var bot = new SunBot(config);
            var data = new WeatherData { Temperature = 25.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("SunBot activated!", output);
        }

        [Fact]
        public void Update_Disabled_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = false, TemperatureThreshold = 30.0, Message = "Sun Alert" };
            var bot = new SunBot(config);
            var data = new WeatherData { Temperature = 35.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("SunBot activated!", output);
        }
    }
}
