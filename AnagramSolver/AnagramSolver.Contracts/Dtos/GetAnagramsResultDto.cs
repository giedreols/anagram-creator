namespace AnagramSolver.Contracts.Dtos;

public class GetAnagramsResultDto
{
    public AnagramViewDto WordAndAnagrams { get; set; } = new AnagramViewDto();

    public IList<ErrorMessageEnum> ErrorMessages { get; set; } = new List<ErrorMessageEnum>();
}