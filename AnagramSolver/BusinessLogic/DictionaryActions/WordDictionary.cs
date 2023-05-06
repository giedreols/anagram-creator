using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace BusinessLogic.DictionaryActions
{
    public class WordDictionary : IWordRepository
    {
        private readonly IFileReader fileReader;


        public WordDictionary(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        // kaip logiskiau - ar metode saugot kintamuosius, ar klaseje? privacius metodus geriau void ar return tipo daryt?
        // ar man isvis yra prasme turet zodyno modeli, kuri iskart konvertuoju i anagramos modeli? ar geriau stringa iskart konvertuoti i anagramos modeli?
        // o gal yra prasme - nes bus paprasciau kitokio formato zodyno faila naudot?
        public ImmutableList<AnagramWord> GetWords()
        {
            ReadOnlyCollection<DictWord> words = ReadWords();
            return ConvertDictionaryWordsToAnagramWords(words);
        }

        private static ImmutableList<AnagramWord> ConvertDictionaryWordsToAnagramWords(ReadOnlyCollection<DictWord> words)
        {
            List<AnagramWord> tempList = new();

            foreach (var word in words)
            {
                tempList.Add(new AnagramWord(word.MainForm));
                tempList.Add(new AnagramWord(word.AnotherForm));
            }

            tempList = (tempList.DistinctBy(word => word.LowerCaseForm).ToList()).OrderByDescending(word => word.MainForm.Length).ToList();

            return tempList.ToImmutableList();
        }

        private ReadOnlyCollection<DictWord> ReadWords()
        {
            IList<string> list = fileReader.ReadFile();

            List<DictWord> tempList = new();

            foreach (string line in list)
            {
                string[] fields = line.Split('\t');
                DictWord word = new(fields[0], fields[1], fields[2]);
                tempList.Add(word);
            }

            return new ReadOnlyCollection<DictWord>(tempList.Distinct().ToList());
        }
    }
}
