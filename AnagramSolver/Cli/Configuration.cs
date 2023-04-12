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

            try
            {
                TotalAmount = int.Parse(config["AnagramSettings:TotalAmount"]);
                MinLength = int.Parse(config["AnagramSettings:MinLength"]);
            }
            catch (Exception) // eating of exceptions is not recommended. If user puts string instead of number in config it should not silently fail. 
                              // But if you want to silently fail look at TryParse :)  
            {
                TotalAmount = 1000;
                MinLength = 1;
            }
        }

    }
}