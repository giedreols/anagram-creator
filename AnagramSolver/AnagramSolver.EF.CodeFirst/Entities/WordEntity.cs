namespace AnagramSolver.EF.CodeFirst.Entities;

public partial class WordEntity
{
    public int Id { get; set; }

    public string MainForm { get; set; } = string.Empty;

    public string OtherForm { get; set; } = string.Empty;

    public string OrderedForm { get; set; } = string.Empty;

    public int? PartOfSpeechId { get; set; }

    public virtual PartOfSpeechEntity? PartOfSpeech { get; set; }
}
