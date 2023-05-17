namespace AnagramSolver.Contracts.Interfaces
{
	public interface IAnagramGenerator
	{
		List<string> GetAnagrams(string inputWord);
	}
}
