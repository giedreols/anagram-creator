namespace AnagramSolver.Contracts.Models
{
	public class FullWordModel
	{
		public int Id { get; set; }
		public string MainForm { get; set; }
		public string OtherForm { get; set; }
		public string PartOfSpeech { get; set; }

        public FullWordModel(string mainForm, string otherForm, string partOfSpeech)
        {
			MainForm = mainForm;
			OtherForm = otherForm;
			PartOfSpeech = partOfSpeech;
        }

		public FullWordModel(int id, string mainForm)
		{
			Id = id;
			MainForm = mainForm;
		}

		public FullWordModel(string mainForm)
		{
			MainForm = mainForm;
		}
	}
}
