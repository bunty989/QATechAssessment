using Microsoft.Extensions.Configuration;

namespace APIAutomation
{
    internal class ConfigHelper
    {
        public static string ConfigSetting(string configType, string keyValue)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            return config.GetValue<string>(configType + keyValue);
        }
    }
}
