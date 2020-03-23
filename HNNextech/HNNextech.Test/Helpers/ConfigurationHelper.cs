using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HNNextech.Test.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetTestConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return configBuilder.Build();
        }

        public static IConfiguration GetBadTestConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("badappsettings.json");

            return configBuilder.Build();
        }

        public static IConfiguration GetEmptyConfiguration()
        {
            return new ConfigurationBuilder().Build();
        }
    }
}
