namespace AnagramSolver.EF.CodeFirst.Entities;

public partial class PartOfSpeechEntity
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string? FullWord { get; set; }

    public virtual ICollection<WordEntity> Words { get; set; } = new List<WordEntity>();
}
