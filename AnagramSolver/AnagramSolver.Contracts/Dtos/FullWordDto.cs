namespace AnagramSolver.Contracts.Dtos
{
    public class FullWordDto
    {
        public int Id { get; set; }
        public string MainForm { get; private set; }
        public string OtherForm { get; private set; }
        public string? PartOfSpeechAbbreviation { get; private set; }
        public int PartOfSpeechId { get; set; }


        public FullWordDto(string mainForm, string otherForm, string partOfSpeechAbbreviation)
        {
            MainForm = mainForm;
            OtherForm = otherForm;
            PartOfSpeechAbbreviation = partOfSpeechAbbreviation;
        }

        public FullWordDto(int wordId, string anyForm)
        {
            Id = wordId;
            MainForm = anyForm;
            OtherForm = anyForm;
        }

        public FullWordDto(string anyForm)
        {
            MainForm = anyForm;
            OtherForm = anyForm;
        }
    }
}
