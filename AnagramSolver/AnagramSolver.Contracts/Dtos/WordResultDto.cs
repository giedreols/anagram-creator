using AnagramSolver.Contracts.Dtos.Obsolete;

namespace AnagramSolver.Contracts.Dtos;

// galima butu perdaryti, kad erroras butu atskirai
public class WordResultDto
{
    public int Id { get; set; }

    public string Word { get; set; }

    public string? ErrorMessage { get; set; }
}

public class ErrorMessages
{
    public const string TooShort = "Žodis per trumpas.";
    public const string InvalidChars = "Žodis turi neleistinų simbolių.";
    public const string TooLong = "Žodis per ilgas.";
    public const string Empty = "Neįvedėte žodžio.";
    public const string AlreadyExists = "Toks žodis jau yra.";
    public const string UnknowReason = "Išsaugoti nepavyko.";
}