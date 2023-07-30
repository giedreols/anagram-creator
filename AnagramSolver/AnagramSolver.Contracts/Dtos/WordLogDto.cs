namespace AnagramSolver.Contracts.Dtos
{
    public class WordLogDto
    {
        public string UserIp { get; set; }
        public WordOpEnum Operation { get; set; }
        public int WordId { get; set; }

        public WordLogDto(string userIp, WordOpEnum operation, int wordId)
        {
            UserIp = userIp;
            Operation = operation;
            WordId = wordId;
        }
    }

    public enum WordOpEnum
    {
        ADD,
        EDIT,
        DELETE
    }
}
