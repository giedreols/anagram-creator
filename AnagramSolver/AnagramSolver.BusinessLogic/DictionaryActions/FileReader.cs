using AnagramSolver.Contracts.Interfaces;
using System.IO;

namespace AnagramSolver.BusinessLogic.DictionaryActions
{
	public class FileReader : IFileReader
	{
		private static string _path;

		public FileReader()
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			try
			{
				if (currentDirectory.Contains("WebApp"))
				{

					_path = Path.Combine(Directory.GetParent(currentDirectory).FullName + "\\AnagramSolver.BusinessLogic\\bin\\Debug\\net7.0");
				}
				else if (currentDirectory.Contains("Cli"))
				{
					_path = currentDirectory.Replace("Cli", "BusinessLogic");

				}

				_path += "\\Files\\zodynas.txt";
			}
			catch
			{
				throw new ArgumentException("Could not find file path");
			}
		}

		public string ReadFile()
		{
			string text;

			try
			{
				text = File.ReadAllText(_path);
			}
			catch
			{
				throw new IOException("Could not get words from the dictionary.");
			}

			return text;
		}

		public void WriteFile(string line)
		{
			try
			{
				File.AppendAllText(_path, $"\r\n{line}");
			}
			catch
			{
				throw new IOException("Could not write word into the dictionary.");
			}
		}
	}
}
