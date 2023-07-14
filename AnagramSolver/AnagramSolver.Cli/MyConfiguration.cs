using AnagramSolver.Contracts.Dtos;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AnagramSolver.Cli
{
    [Obsolete("new implementation uses WebApp")]
    public class MyConfiguration
    {
        public ConfigOptionsDto configOptions { get; set; }

        private IConfigurationRoot _config { get; set; }

        public MyConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var path = Directory.GetCurrentDirectory();
            var newPath = path.Replace("Cli", "WebApp");
            builder.SetBasePath(newPath).AddJsonFile("appsettings.json", optional: false);

            _config = builder.Build();

            configOptions = new ConfigOptionsDto();

            configOptions.TotalAmount = ReadConfiguration("AnagramSettings:TotalAmount");
            configOptions.MinLength = ReadConfiguration("AnagramSettings:MinLength");
            configOptions.MaxLength = ReadConfiguration("AnagramSettings:MaxLength");
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