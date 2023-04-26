namespace Contracts.Models

{
    public class AnagramWord
    {

        public string MainForm { get; set; }

        public string LowerCaseForm { get; set; }

        public string OrderedForm { get; set; }

        public AnagramWord(string mainForm)
        {
            MainForm = mainForm;
            LowerCaseForm = mainForm.ToLower();
            OrderedForm = new String(mainForm.ToLower().OrderByDescending(a => a).ToArray());
        }
    }
}

