namespace AnagramSolver.Contracts.Models
{
	public class DictionaryWordModel
	{
		public int Id { get; set; }
		public string MainForm { get; set; }
		public string OtherForm { get; set; }
		public string PartOfSpeech { get; set; }

        public DictionaryWordModel(string mainForm, string otherForm, string partOfSpeech)
        {
			MainForm = mainForm;
			OtherForm = otherForm;
			PartOfSpeech = partOfSpeech;
        }

		public DictionaryWordModel(string mainForm, string otherForm)
		{
			MainForm = mainForm;
			OtherForm = otherForm;
		}

		public DictionaryWordModel(int id, string mainForm)
		{
			Id = id;
			MainForm = mainForm;
		}
	}
}
