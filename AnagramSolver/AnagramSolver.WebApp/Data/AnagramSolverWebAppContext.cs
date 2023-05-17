using AnagramSolver.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.WebApp.Data
{
	public class AnagramSolverWebAppContext : DbContext
	{
		public AnagramSolverWebAppContext(DbContextOptions<AnagramSolverWebAppContext> options)
			: base(options)
		{
		}

		public DbSet<AnagramWordsModel> AnagramWords { get; set; } = default!;
	}
}
