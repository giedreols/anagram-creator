namespace Contracts.Interfaces
{
    public interface IRenderer
    {
        string GetWord();

        void RejectWord(string reason);

        void ShowAnagrams(IList<string> anagrams);

        bool DoRepeat();

    }
}
