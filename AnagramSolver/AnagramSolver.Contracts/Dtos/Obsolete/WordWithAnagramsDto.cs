namespace AnagramSolver.Contracts.Dtos.Obsolete

{
    [Obsolete]
    public class WordWithAnagramsDto
    {
        public string Word { get; private set; }

        public IEnumerable<string> Anagrams { get; set; }

        public WordWithAnagramsDto(string word, IEnumerable<string> anagrams)
        {
            Word = word;
            Anagrams = anagrams;
        }
    }
}