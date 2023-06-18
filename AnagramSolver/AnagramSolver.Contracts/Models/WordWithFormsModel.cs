namespace AnagramSolver.Contracts.Models

{
	public class WordWithFormsModel
	{

		public string MainForm { get; set; }

		public string LowerCaseForm { get; set; }

		public string OrderedForm { get; set; }

		public WordWithFormsModel(string mainForm)
		{
			MainForm = mainForm;
			LowerCaseForm = mainForm.ToLower();
			OrderedForm = new string(mainForm.ToLower().OrderByDescending(a => a).ToArray());
		}
	}
}

