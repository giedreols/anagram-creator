namespace AnagramSolver.Contracts.Interfaces
{
	public interface IFileReader
	{
		byte[] GetFile();

		string ReadFile();

		void WriteFile(string lines);
	}
}
