using Xunit;
using Real_time_weather_monitoring.Bots;
using Real_time_weather_monitoring.Models;
using System.IO;
using System;

namespace Real_time_weather_monitoring.Tests.Bots
{
    [Trait("Category", "RainBot")]
    public class RainBotTests
    {
        [Fact]
        public void Update_EnabledAndHumidityAboveThreshold_PrintsActivation()
        {
            var config = new BotConfig { Enabled = true, HumidityThreshold = 50.0, Message = "Rain Alert" };
            var bot = new RainBot(config);
            var data = new WeatherData { Humidity = 60.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.Contains("RainBot activated!", output);
            Assert.Contains("Rain Alert", output);
        }

        [Fact]
        public void Update_EnabledButHumidityBelowThreshold_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = true, HumidityThreshold = 50.0, Message = "Rain Alert" };
            var bot = new RainBot(config);
            var data = new WeatherData { Humidity = 40.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("RainBot activated!", output);
        }

        [Fact]
        public void Update_Disabled_DoesNotPrint()
        {
            var config = new BotConfig { Enabled = false, HumidityThreshold = 50.0, Message = "Rain Alert" };
            var bot = new RainBot(config);
            var data = new WeatherData { Humidity = 60.0 };
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bot.Update(data);
            var output = sw.ToString();
            Assert.DoesNotContain("RainBot activated!", output);
        }
    }
}
