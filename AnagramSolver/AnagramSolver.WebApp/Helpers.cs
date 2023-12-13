using AnagramSolver.WebApp.Models;

namespace AnagramSolver.WebApp
{
    public static class Helpers
    {
        public static string ConvertErrorEnumToMessage(ErrorMessageEnum errorMessageEnum)
        {
            Dictionary<ErrorMessageEnum, string> errors = new();
            errors.Add(ErrorMessageEnum.TooShort, "Žodis per trumpas.");
            errors.Add(ErrorMessageEnum.InvalidChars, "Žodis turi neleistinų simbolių.");
            errors.Add(ErrorMessageEnum.TooLong, "Žodis per ilgas.");
            errors.Add(ErrorMessageEnum.Empty, "Neįvedėte žodžio.");
            errors.Add(ErrorMessageEnum.AlreadyExists, "Toks žodis jau yra.");
            errors.Add(ErrorMessageEnum.UnknowReason, "Nežinoma klaida.");

            return errors.FirstOrDefault(e => e.Key == errorMessageEnum).Value;
        }
    }
}
