using System.Net;

namespace AnagramSolver.Contracts.Dtos;

public class AnagramViewDto()
{
    public string Word { get; set; }

    public int WordId { get; set; }

    public IEnumerable<string> Anagrams { get; set; } = new List<string>();
}
