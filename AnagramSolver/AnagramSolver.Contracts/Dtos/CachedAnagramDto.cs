namespace AnagramSolver.Contracts.Dtos
{
    public class CachedAnagramDto
    {
        public bool IsCached { get; set; }

        public IList<string> Anagrams { get; set; }

        public CachedAnagramDto(bool isCached, IList<string> anagrams)
        {
            IsCached = isCached;
            Anagrams = anagrams;
        }
    }
}
