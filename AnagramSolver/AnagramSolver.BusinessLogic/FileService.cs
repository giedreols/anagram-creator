using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
	public class FileService
    {
        private readonly IWordRepository _wordRepo;

        public FileService(IWordRepository wordRepo)
        {
            _wordRepo = wordRepo;
        }

        public byte[] GetFileWithWords()
        {
            IList<string> wordList = _wordRepo.GetWords().ToList();

            return wordList.ConvertListToByteArr();
        }
    }
}
