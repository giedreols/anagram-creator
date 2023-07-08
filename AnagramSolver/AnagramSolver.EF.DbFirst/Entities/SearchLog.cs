using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DbFirst.Entities;

public partial class SearchLog
{
    public int Id { get; set; }

    public string UserIp { get; set; } = null!;

    public string SearchWord { get; set; } = null!;

    public DateTime? TimeStamp { get; set; }
}
