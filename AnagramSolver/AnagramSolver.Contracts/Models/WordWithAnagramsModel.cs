﻿namespace AnagramSolver.Contracts.Models

{
	public class WordWithAnagramsModel
	{
		public string Word { get; set; }

		public IList<string> Anagrams { get; set; }

		public WordWithAnagramsModel(string word, IList<string> anagrams)
		{
			Word = word;
			Anagrams = anagrams;
		}
	}
}