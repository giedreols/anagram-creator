using AnagramSolver.WebApp.Models;

namespace AnagramSolver.WebApp
{
    public static class Helpers
    {
        public static string ConvertErrorEnumToMessage(ErrorMessageEnumModel errorMessageEnum)
        {
            Dictionary<ErrorMessageEnumModel, string> errors = new();
            errors.Add(ErrorMessageEnumModel.TooShort, "Žodis per trumpas.");
            errors.Add(ErrorMessageEnumModel.InvalidChars, "Žodis turi neleistinų simbolių.");
            errors.Add(ErrorMessageEnumModel.TooLong, "Žodis per ilgas.");
            errors.Add(ErrorMessageEnumModel.Empty, "Neįvedėte žodžio.");
            errors.Add(ErrorMessageEnumModel.AlreadyExists, "Toks žodis jau yra.");
            errors.Add(ErrorMessageEnumModel.UnknownReason, "Nežinoma klaida.");

            return errors.FirstOrDefault(e => e.Key == errorMessageEnum).Value;
        }
    }
}
