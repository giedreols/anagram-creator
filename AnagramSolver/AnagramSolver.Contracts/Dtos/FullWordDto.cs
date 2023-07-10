namespace AnagramSolver.Contracts.Dtos
{
    public class FullWordDto
    {
        public int? Id { get; set; }
        public string MainForm { get; set; }
        public string OtherForm { get; set; }
        public string? PartOfSpeechAbbreviation { get; set; }

        public FullWordDto(string mainForm, string otherForm, string partOfSpeechAbbreviation)
        {
            MainForm = mainForm;
            OtherForm = otherForm;
            PartOfSpeechAbbreviation = partOfSpeechAbbreviation;
        }

        public FullWordDto(string anyForm)
        {
            MainForm = anyForm;
            OtherForm = anyForm;
        }
    }
}
