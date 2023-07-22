namespace AnagramSolver.Contracts.Dtos
{
    public class SearchLogDto
    {
        public string UserIp { get; private set; }

        public DateTime? TimeStamp { get; private set; }

        public string Word { get; private set; }

        public SearchLogDto(string userIp, DateTime? timeStamp, string word)
        {
            UserIp = userIp;
            TimeStamp = timeStamp;
            Word = word;
        }
    }
}
