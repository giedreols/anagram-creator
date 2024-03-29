﻿namespace AnagramSolver.EF.DbFirst.Entities;

public partial class Word
{
    public int Id { get; set; }

    public string MainForm { get; set; } = String.Empty;

    public string OtherForm { get; set; } = String.Empty;

    public string OrderedForm { get; set; } = String.Empty;

    public int? PartOfSpeechId { get; set; }

    public virtual PartOfSpeech? PartOfSpeech { get; set; }

    public bool IsDeleted { get; set; } = false;
}
