using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class PartOfSpeechRepo : IPartOfSpeechRespository
    {
        public int InsertPartOfSpeechIfDoesNotExist(string abbr)
        {
            using var context = new AnagramSolverDataContext();

            PartOfSpeech parameters = new()
            {
                Abbreviation = abbr,
                Id = context.PartsOfSpeech
                .Where(x => x.Abbreviation.Equals(abbr))
                .Select(x => x.Id).FirstOrDefault()
            };

            if (parameters.Id == 0)
            {
                context.PartsOfSpeech.Add(parameters);
                context.SaveChanges();
            }

            return parameters.Id;
        }
    }
}

