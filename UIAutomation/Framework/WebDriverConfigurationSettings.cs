using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UIAutomation.Framework
{
    public static class WebDriverConfigurationSettings
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
