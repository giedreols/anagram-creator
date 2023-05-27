using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;

namespace AnagramSolver.Cli
{
	public class MyConfiguration
	{
		public int TotalAmount { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		private IConfigurationRoot _config { get; set; }

		public MyConfiguration()
		{
			var builder = new ConfigurationBuilder();
			var path = Directory.GetCurrentDirectory();
			var newPath = path.Replace("Cli", "WebApp");
			builder.SetBasePath(newPath).AddJsonFile("appsettings.json", optional: false);

			_config = builder.Build();

			TotalAmount = ReadConfiguration("AnagramSettings:TotalAmount");
			MinLength = ReadConfiguration("AnagramSettings:MinLength");
			MaxLength = ReadConfiguration("AnagramSettings:MaxLength");
		}

		private int ReadConfiguration(string settings)
		{
			return int.TryParse(_config[settings], out int value)
				? value
				: throw new ConfigurationErrorsException($"{settings} is not set correctly in configuration");
		}

		static string GetProjectName(string baseDirectory)
		{
			int lastSeparatorIndex = baseDirectory.TrimEnd('\\').LastIndexOf('\\');
			return baseDirectory.Substring(lastSeparatorIndex + 1);
		}
	}
}