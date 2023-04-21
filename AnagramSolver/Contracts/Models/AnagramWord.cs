namespace Contracts.Models

{
    public class AnagramWord
    {
        public string MainForm { get; set; }

        public string LowerCaseForm { get; set; }

        public string OrderedForm { get; set; }


        public string Type { get; set; }


        public AnagramWord(string mainForm, string type)
        {
            MainForm = mainForm;
            LowerCaseForm = mainForm.ToLower();
            OrderedForm = new String(mainForm.ToLower().OrderBy(a => a).ToArray());
            Type = type;
        }
    }
}