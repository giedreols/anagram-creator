namespace AnagramSolver.Contracts.Models
{
	public class FullWordModel
	{
		public int? Id { get; set; }
		public string MainForm { get; set; }
		public string OtherForm { get; set; }
		public string? PartOfSpeechAbbreviation { get; set; }

		public FullWordModel(string mainForm, string otherForm, string partOfSpeechAbbreviation)
		{
			MainForm = mainForm;
			OtherForm = otherForm;
			PartOfSpeechAbbreviation = partOfSpeechAbbreviation;
		}

		public FullWordModel(string anyForm)
		{
			MainForm = anyForm;
			OtherForm = anyForm;
		}
	}
}
