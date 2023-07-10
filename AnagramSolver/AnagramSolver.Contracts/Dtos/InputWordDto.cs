namespace AnagramSolver.Contracts.Dtos


{
    public class InputWordDto
    {
        public string Word { get; set; }

        public string? InvalidityReason { get; set; }

        public InputWordDto(string word)
        {
            Word = word;
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
}