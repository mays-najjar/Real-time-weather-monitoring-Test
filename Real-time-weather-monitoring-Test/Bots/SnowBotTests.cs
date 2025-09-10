using Xunit;
using Real_time_weather_monitoring.Bots;
using Real_time_weather_monitoring.Models;
using System.IO;
using System;

namespace Real_time_weather_monitoring.Tests.Bots
{
    [Trait("Category", "SnowBot")]
    public class SnowBotTests
    {
        [Fact]
        public void Update_EnabledAndTemperatureBelowThreshold_PrintsActivation()
        {
            var config = new BotConfig { Enabled = true, TemperatureThreshold = 0.0, Message = "Snow Alert" };
            var bot = new SnowBot(config);
            var data = new WeatherData { Temperature = -5.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.Contains("SnowBot activated!", output);
            Assert.Contains("Snow Alert", output);
        }

        [Fact]
        public void Update_EnabledButTemperatureAboveThreshold_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = true, TemperatureThreshold = 0.0, Message = "Snow Alert" };
            var bot = new SnowBot(config);
            var data = new WeatherData { Temperature = 5.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("SnowBot activated!", output);
        }

        [Fact]
        public void Update_Disabled_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = false, TemperatureThreshold = 0.0, Message = "Snow Alert" };
            var bot = new SnowBot(config);
            var data = new WeatherData { Temperature = -5.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("SnowBot activated!", output);
        }
    }
}
