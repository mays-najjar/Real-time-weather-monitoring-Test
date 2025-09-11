using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Real_time_weather_monitoring.Services;
using Xunit;
using System.IO;
using System;

namespace Real_time_weather_monitoring_Test.Services
{
    [Trait("Category", "ConfigurationService")]
    public class ConfigurationServiceTests : IDisposable
    {
        private readonly string _tempFilePath;
        private readonly ConfigurationService _service = new();

        public ConfigurationServiceTests()
        {
            _tempFilePath = Path.GetTempFileName();
        }

        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        [Fact]
        public void LoadConfiguration_ValidFile_ReturnsConfiguration()
        {
            var json = "{\"RainBot\":{\"Enabled\":true,\"Message\":\"Rain\"},\"SunBot\":{\"Enabled\":false},\"SnowBot\":{\"Enabled\":true}}";
            File.WriteAllText(_tempFilePath, json);
            var result = _service.LoadConfiguration(_tempFilePath);
            Assert.True(result.RainBot.Enabled);
            Assert.Equal("Rain", result.RainBot.Message);
            Assert.False(result.SunBot.Enabled);
            Assert.True(result.SnowBot.Enabled);
        }

        [Fact]
        public void LoadConfiguration_FileNotFound_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => _service.LoadConfiguration("nonexistent.json"));
        }

        [Fact]
        public void LoadConfiguration_InvalidJson_ThrowsInvalidDataException()
        {
            var json = "invalid json";
            File.WriteAllText(_tempFilePath, json);
            Assert.Throws<System.IO.InvalidDataException>(() => _service.LoadConfiguration(_tempFilePath));
        }


    }
}