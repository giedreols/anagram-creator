using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DbFirst.Entities;

public partial class Anagram
{
    public int Id { get; set; }

    public string SearchWord { get; set; } = null!;

    public DateTime? TimeStamp { get; set; }

    public int? WordId { get; set; }

    public virtual Word? Word { get; set; }
}
