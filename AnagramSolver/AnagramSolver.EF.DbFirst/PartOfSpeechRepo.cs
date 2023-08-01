using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class PartOfSpeechRepo : IPartOfSpeechRespository
    {
        private readonly AnagramSolverDataContext _context;

        public PartOfSpeechRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public int InsertPartOfSpeechIfDoesNotExist(string abbr)
        {
            PartOfSpeech parameters = new()
            {
                Abbreviation = abbr,
                Id = _context.PartsOfSpeech
                .Where(x => x.Abbreviation.Equals(abbr))
                .Select(x => x.Id).FirstOrDefault()
            };

            if (parameters.Id == 0)
            {
                _context.PartsOfSpeech.Add(parameters);
                _context.SaveChanges();
            }

            return parameters.Id;
        }
    }
}

