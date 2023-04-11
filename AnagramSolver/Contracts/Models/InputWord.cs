namespace Contracts.Models


{
    public class InputWord
    {
        public string MainForm { get; set; }

        public string? InvalidityReason { get; set; }

        public HashSet<string>? Permutations { get; set; }

        public InputWord(string mainForm)
        {
            MainForm = mainForm;
        }
    }
}