namespace AnagramSolver.Contracts.Interfaces
{
	public interface IAnagramGenerator
	{
		IList<string> GetAnagrams(string inputWord);
	}
}
