namespace Contracts.Models

{
    public class DictWord
    {
        public string MainForm { get; set; }

        public string Type { get; set; }

        public string AnotherForm { get; set; }

        public DictWord(string mainForm, string type, string anotherForm)
        {
            MainForm = mainForm;
            Type = type;
            AnotherForm = anotherForm;
        }
    }
}