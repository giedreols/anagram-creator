using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DbFirst.Entities;

public partial class PartOfSpeech
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string? FullWord { get; set; }

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
