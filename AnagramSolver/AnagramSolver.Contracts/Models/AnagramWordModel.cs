namespace AnagramSolver.Contracts.Models

{
	public class AnagramWordModel
	{

		public string MainForm { get; set; }

		public string LowerCaseForm { get; set; }

		public string OrderedForm { get; set; }

		public AnagramWordModel(string mainForm)
		{
			MainForm = mainForm;
			LowerCaseForm = mainForm.ToLower();
			OrderedForm = new string(mainForm.ToLower().OrderByDescending(a => a).ToArray());
		}
	}
}

