using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class FileService : IFileService
    {
        private readonly IWordRepository _wordRepo;

        public FileService(IWordRepository wordRepo)
        {
            _wordRepo = wordRepo;
        }

        public async Task<byte[]> GetFileWithWords()
        {
            var words = await _wordRepo.GetWordsAsync();

            byte[] wordList = words.Values.ToList().ConvertListToByteArr();

            return wordList;
        }
    }
}
