namespace AnagramSolver.Contracts.Dtos
{
    public class ConfigOptionsDto
    {
        public int TotalAmount { get; private set; }
        public int MinLength { get; private set; }
        public int MaxLength { get; private set; }
        public int SearchCount { get; private set; }
        public string ConnString { get; private set; }


        public ConfigOptionsDto(int totalAmount, int minLength, int maxLength, int searchCount, string connString)
        {
            TotalAmount = totalAmount;
            MinLength = minLength;
            MaxLength = maxLength;
            SearchCount = searchCount;
            ConnString = connString;
        }
    }
}
