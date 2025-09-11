using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Real_time_weather_monitoring.Bots;
using Real_time_weather_monitoring.Models;
using Real_time_weather_monitoring.Services;
using Xunit;

namespace Real_time_weather_monitoring_Test.Services
{
    [Trait("Category", "BotFactory")]
    public class BotFactoryTests
    {
        private readonly BotFactory _factory = new();

        [Fact]
        public void CreateBots_AllBotsConfigured_ReturnsAllBots()
        {
            var config = new BotConfiguration
            {
                RainBot = new BotConfig { Enabled = true },
                SunBot = new BotConfig { Enabled = true },
                SnowBot = new BotConfig { Enabled = true }
            };
            var bots = _factory.CreateBots(config);
            Assert.Equal(3, bots.Count);
            Assert.Contains(bots, b => b is RainBot);
            Assert.Contains(bots, b => b is SunBot);
            Assert.Contains(bots, b => b is SnowBot);
        }

        [Fact]
        public void CreateBots_NoBotsConfigured_ReturnsEmptyList()
        {
            var config = new BotConfiguration
            {
                RainBot = null,
                SunBot = null,
                SnowBot = null
            };
            var bots = _factory.CreateBots(config);
            Assert.Empty(bots);
        }

        [Fact]
        public void CreateBots_SomeBotsConfigured_ReturnsOnlyConfigured()
        {
            var config = new BotConfiguration
            {
                RainBot = new BotConfig { Enabled = true },
                SunBot = null,
                SnowBot = new BotConfig { Enabled = true }
            };
            var bots = _factory.CreateBots(config);
            Assert.Equal(2, bots.Count);
            Assert.Contains(bots, b => b is RainBot);
            Assert.Contains(bots, b => b is SnowBot);
        }
    }
}