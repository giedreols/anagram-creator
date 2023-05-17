namespace AnagramSolverWebApp.Models;

public class WordFailedToSaveModel
{
	public string Message { get; set; }

    public WordFailedToSaveModel(string message)
    {
        Message = message;
    }
}
