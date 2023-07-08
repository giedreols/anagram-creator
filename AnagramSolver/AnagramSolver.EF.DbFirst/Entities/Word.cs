using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DbFirst.Entities;

public partial class Word
{
    public int Id { get; set; }

    public string MainForm { get; set; } = null!;

    public string OtherForm { get; set; } = null!;

    public int? PartOfSpeechId { get; set; }

    public virtual ICollection<Anagram> Anagrams { get; set; } = new List<Anagram>();

    public virtual PartsOfSpeech? PartOfSpeech { get; set; }
}
