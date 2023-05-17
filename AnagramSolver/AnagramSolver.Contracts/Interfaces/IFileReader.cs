namespace AnagramSolver.Contracts.Interfaces
{
	public interface IFileReader
	{
		string ReadFile();

		void WriteFile(string lines);
	}
}
