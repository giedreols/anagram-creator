namespace Contracts.Models


{
    public class InputWord
    {
        public string MainForm { get; set; }

        public string? InvalidityReason { get; set; }

        public InputWord(string mainForm)
        {
            MainForm = mainForm;
        }
    }


    public static class WordRejectionReasons
    {
        public const string TooShort = "Word is too short.";
        public const string InvalidCharacters = "Word contains invalid characters.";
    }
}