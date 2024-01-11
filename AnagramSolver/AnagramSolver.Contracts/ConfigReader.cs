using System.Configuration;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.Contracts
{
    public class ConfigReader : IConfigReader
    {
        public ConfigOptionsDto? ConfigOptions { get; set; }

        private IConfigurationRoot _config { get; set; }

        public ConfigReader()
        {
            var builder = new ConfigurationBuilder();
            var path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path).AddJsonFile("appsettings.json", optional: false);

            _config = builder.Build();

            ConfigOptions = new ConfigOptionsDto(ReadConfigurationInt("AnagramSettings:TotalAmount")
                , ReadConfigurationInt("AnagramSettings:MinLength")
                , ReadConfigurationInt("AnagramSettings:MaxLength")
                , ReadConfigurationInt("AnagramSettings:SearchCount")
                , ReadConfigurationString("ConnectionStrings:AnagramSolverWebAppContext"));
        }

        private int ReadConfigurationInt(string settings)
        {
            return int.TryParse(_config[settings], out int value)
                ? value
                : throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
        }

        private static string ReadConfigurationString(string settings)
        {
            return !string.IsNullOrEmpty(settings)
                ? settings
                : throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
        }
    }
}