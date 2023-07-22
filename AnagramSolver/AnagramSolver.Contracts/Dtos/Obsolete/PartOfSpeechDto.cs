namespace AnagramSolver.Contracts.Dtos.Obsolete
{
    [Obsolete("only for obsolete parts and db seeding from txt file")]
    public class PartOfSpeechDto
    {
        public string Abbreviation { get; set; }
        public string? FullWord { get; set; }
    }
}
