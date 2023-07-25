namespace AnagramSolver.EF.DbFirst.Entities;

public partial class WordLog
{
    public int Id { get; set; }

    public string UserIp { get; set; } = String.Empty;

    public int WordId { get; set; }

    public virtual Word? Word { get; set; }

    public WordOpEnum Operation { get; set; }

    public DateTime? TimeStamp { get; set; }
}

public enum WordOpEnum
{
    ADD,
    EDIT,
    DELETE
}
