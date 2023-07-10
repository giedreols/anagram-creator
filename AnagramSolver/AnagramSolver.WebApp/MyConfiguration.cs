using AnagramSolver.Contracts.Dtos;
using System.Configuration;

namespace AnagramSolver.WebApp
{
    public class MyConfiguration
    {
        public ConfigOptionsDto ConfigOptions { get; private set; }

        private IConfigurationRoot _config { get; set; }

        public MyConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path).AddJsonFile("appsettings.json", optional: false);

            _config = builder.Build();

            ConfigOptions = new ConfigOptionsDto();

            ConfigOptions.TotalAmount = ReadConfiguration("AnagramSettings:TotalAmount");
            ConfigOptions.MinLength = ReadConfiguration("AnagramSettings:MinLength");
            ConfigOptions.MaxLength = ReadConfiguration("AnagramSettings:MaxLength");
        }

        private int ReadConfiguration(string settings)
        {
            return int.TryParse(_config[settings], out int value)
                ? value
                : throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
        }
    }
}