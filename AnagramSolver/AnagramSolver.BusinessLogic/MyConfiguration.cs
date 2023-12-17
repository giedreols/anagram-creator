using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AnagramSolver.BusinessLogic
{
    public class MyConfiguration : IMyConfiguration
    {
        public ConfigOptionsDto? ConfigOptions { get; set; }

        private IConfigurationRoot _config { get; set; }

        public MyConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path).AddJsonFile("appsettings.json", optional: false);

            _config = builder.Build();

            ConfigOptions = new ConfigOptionsDto(ReadConfiguration("AnagramSettings:TotalAmount")
                , ReadConfiguration("AnagramSettings:MinLength")
                , ReadConfiguration("AnagramSettings:MaxLength")
                , ReadConfiguration("AnagramSettings:SearchCount"));
        }

        private int ReadConfiguration(string settings)
        {
            return int.TryParse(_config[settings], out int value)
                ? value
                : throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
        }

        private static string GetProjectName(string baseDirectory)
        {
            int lastSeparatorIndex = baseDirectory.TrimEnd('\\').LastIndexOf('\\');
            return baseDirectory.Substring(lastSeparatorIndex + 1);
        }
    }
}