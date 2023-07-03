namespace AnagramSolver.Contracts.Dtos

{
	public class WordWithFormsDto
	{

		public string MainForm { get; set; }

		public string LowerCaseForm { get; set; }

		public string OrderedForm { get; set; }

		public WordWithFormsDto(string mainForm)
		{
			MainForm = mainForm;
			LowerCaseForm = mainForm.ToLower();
			OrderedForm = new string(mainForm.ToLower().OrderByDescending(a => a).ToArray());
		}
	}
}

