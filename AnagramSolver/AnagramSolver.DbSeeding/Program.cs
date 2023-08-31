using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DbSeeding;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddDbContext<AnagramSolverDataContext>(options =>
        options.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True;TrustServerCertificate=true"))
    .AddScoped<IWordRepository, WordRepo>()
    .AddScoped<IPartOfSpeechRespository, PartOfSpeechRepo>()
    .BuildServiceProvider();

var wordRepo = serviceProvider.GetService<IWordRepository>();
var partOfSpeechRepo = serviceProvider.GetService<IPartOfSpeechRespository>();


SeedOneLineDictionary seeder = new(wordRepo, partOfSpeechRepo);

seeder.Seed();