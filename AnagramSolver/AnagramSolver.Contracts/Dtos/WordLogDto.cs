namespace AnagramSolver.Contracts.Dtos
{
    public class WordLogDto
    {
        public string UserIp { get; set; }
        public WordOpEnum Operation { get; set; }
        public int WordId { get; set; }

        public DateTime TimeStamp { get; set; }


        public WordLogDto(string userIp, WordOpEnum operation, int wordId, DateTime timeStamp)
        {
            UserIp = userIp;
            Operation = operation;
            WordId = wordId;
            TimeStamp = timeStamp;
        }
    }

    public enum WordOpEnum
    {
        Add,
        Edit,
        Delete
    }
}
