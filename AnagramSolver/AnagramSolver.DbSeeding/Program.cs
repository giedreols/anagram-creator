using AnagramSolver.EF.DbFirst;
using AnagramSolver.DbSeeding;



SeedOneLineDictionary.Seed();


new DbFirstWordsActions().AddOrderedFormForAllWords();