namespace AnagramSolver.Contracts.Dtos;

public class NewWordDto
{
	public bool IsSaved { get; set; }

	public string? ErrorMessage { get; set; }

	public WordWithAnagramsDto? AnagramWords { get; set; }
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