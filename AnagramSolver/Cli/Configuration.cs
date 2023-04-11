using Microsoft.Extensions.Configuration;

namespace BusinessLogic
{
    public class Configuration
    {
        public int TotalAmount { get; private set; }
        public int MinLength { get; private set; }

        public void ReadSettings()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            TotalAmount = int.Parse(config["AnagramSettings:TotalAmount"]);
            MinLength = int.Parse(config["AnagramSettings:MinLength"]);
        }

    }
}