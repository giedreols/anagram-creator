namespace AnagramSolver.Contracts.Models

{
	public class DictWord
	{
		public string MainForm { get; private set; }

		public string Type { get; private set; }

		public string AnotherForm { get; private set; }

		public DictWord(string mainForm, string type, string anotherForm)
		{
			MainForm = mainForm;
			Type = type;
			AnotherForm = anotherForm;
		}
	}
}