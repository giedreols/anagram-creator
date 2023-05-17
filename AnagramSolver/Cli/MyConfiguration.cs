using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Cli
{
	public class MyConfiguration
	{
		public int TotalAmount { get; private set; }
		public int MinLength { get; private set; }
		public int MaxLength { get; private set; }


		private IConfigurationRoot _config { get; set; }

		public MyConfiguration()
		{
			ConfigurationBuilder builder = new();
			builder.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\AnagamSolverWebApp\\bin\\Debug\\net7.0")
				   .AddJsonFile("appsettings.json", optional: false);

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
	}
}