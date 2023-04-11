namespace Contracts.Models


{
    public class InputWord
    {
        public string MainForm;

        public string InvalidReason;

        public HashSet<string> Permutations;
    }
}