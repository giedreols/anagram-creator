namespace AnagramSolver.Contracts.Dtos;

// galima butu perdaryti, kad erroras butu atskirai
public class WordResultDto
{
    public int Id { get; set; }

    public string Word { get; set; }

    public ErrorMessageEnum ErrorMessage { get; set; }
}