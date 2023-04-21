namespace Contracts.Interfaces
{
    public interface IRenderer
    {
        void ShowHeader();

        string GetWord();

        void RejectWord(string reason);

        void ShowAnagrams(IList<List<string>> anagrams);

        bool DoRepeat();

        void ShowError(string message);

    }
}
