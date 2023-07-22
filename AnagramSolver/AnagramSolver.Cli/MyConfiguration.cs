using AnagramSolver.Contracts.Dtos;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AnagramSolver.Cli
{
	[Obsolete("new implementation uses WebApp")]
	public class MyConfiguration
	{
		public ConfigOptionsDto? ConfigOptions { get; private set; }

		private IConfigurationRoot _config { get; set; }

		public MyConfiguration()
		{
			var builder = new ConfigurationBuilder();
			var path = Directory.GetCurrentDirectory();
			var newPath = path.Replace("Cli", "WebApp");
			builder.SetBasePath(newPath).AddJsonFile("appsettings.json", optional: false);

			_config = builder.Build();

			ConfigOptions = new ConfigOptionsDto(ReadConfiguration("AnagramSettings:TotalAmount")
				, ReadConfiguration("AnagramSettings:MinLength")
				, ReadConfiguration("AnagramSettings:MaxLength"));
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