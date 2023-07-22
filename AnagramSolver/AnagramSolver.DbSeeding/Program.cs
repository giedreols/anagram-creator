using AnagramSolver.DbSeeding;
using AnagramSolver.EF.DbFirst;



SeedOneLineDictionary.Seed();


new WordRepo().AddOrderedFormForAllWords();