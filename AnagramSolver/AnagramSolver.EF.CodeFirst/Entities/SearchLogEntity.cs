namespace AnagramSolver.EF.CodeFirst.Entities;

public partial class SearchLogEntity
{
    public int Id { get; set; }

    public string UserIp { get; set; } = null!;

    public string SearchWord { get; set; } = null!;

    public DateTime? TimeStamp { get; set; }
}
