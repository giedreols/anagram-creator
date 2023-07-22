namespace AnagramSolver.WebApp.Models
{
    public class ErrorModel
    {
        public string Message { get; private set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}
