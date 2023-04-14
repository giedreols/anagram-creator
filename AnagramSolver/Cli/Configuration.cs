using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Cli
{
    public class Configuration
    {
        public int TotalAmount { get; private set; }
        public int MinLength { get; private set; }

        private IConfigurationRoot Config { get; set; }

        public Configuration()
        {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false);

            Config = builder.Build();

            TotalAmount = ReadConfiguration("AnagramSettings:TotalAmount");
            MinLength = ReadConfiguration("AnagramSettings:MinLength");
        }

        private int ReadConfiguration(string settings)
        {
            return int.TryParse(Config[settings], out int value)
                ? value
                : throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
        }
    }
}