using Microsoft.Extensions.Configuration;

namespace Cli
{
    public class Configuration
    {
        public int TotalAmount { get; private set; }
        public int MinLength { get; private set; }

        public Configuration()
        {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot config = builder.Build();

            TotalAmount = int.Parse(config["AnagramSettings:TotalAmount"]);
            MinLength = int.Parse(config["AnagramSettings:MinLength"]);
        }

    }
}