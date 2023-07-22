namespace AnagramSolver.Contracts.Interfaces
{
    [Obsolete]
    public interface IRenderer
    {
        void ShowHeader();

        string GetWord();

        void RejectWord(string reason);

        void ShowAnagrams(IList<string> anagrams);

        bool DoRepeat();

        void ShowError(string message);

    }
}
