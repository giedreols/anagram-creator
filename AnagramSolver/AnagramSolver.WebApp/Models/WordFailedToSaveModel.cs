namespace AnagramSolver.WebApp.Models;

public class WordFailedToSaveModel
{
    public string Message { get; set; }

    public WordFailedToSaveModel(string message)
    {
        Message = message;
    }
}



public static class WordRejectionReasons
{
    public const string TooShort = "Žodis per trumpas.";
    public const string InvalidChars = "Žodis turi neleistinų simbolių.";
    public const string TooLong = "Žodis per ilgas.";
    public const string Empty = "Neįvedėte žodžio.";
    public const string AlreadyExists = "Toks žodis jau yra.";
}