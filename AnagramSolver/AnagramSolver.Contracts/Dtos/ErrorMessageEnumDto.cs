namespace AnagramSolver.Contracts.Dtos
{
    public enum ErrorMessageEnumDto
    {
        Ok,
        TooShort,
        InvalidChars,
        TooLong,
        Empty,
        AlreadyExists,
        SearchLimit,
        UnknowReason
    }

    //TooShort = "Žodis per trumpas.";
    //InvalidChars = "Galima naudoti tik raides.";
    //TooLong = "Žodis per ilgas.";
    //Empty = "Neįvedėte žodžio.";
    //AlreadyExists = "Toks žodis jau yra.";
    //SearchLimit = "Paieškų liminas išnaudotas.";
    //UnknowReason = "Nežinoma klaida.";

}
