namespace AnagramSolver.Contracts.Dtos
{
    public class ConfigOptionsDto
    {
        public int TotalAmount { get; private set; }
        public int MinLength { get; private set; }
        public int MaxLength { get; private set; }
        public int SearchCount { get; private set; }

        public ConfigOptionsDto(int totalAmount, int minLength, int maxLength, int searchCount)
        {
            TotalAmount = totalAmount;
            MinLength = minLength;
            MaxLength = maxLength;
            SearchCount = searchCount;
        }
    }
}
