using Microsoft.EntityFrameworkCore;

namespace AnagamSolverWebApp.Data
{
	public class AnagamSolverWebAppContext : DbContext
	{
		public AnagamSolverWebAppContext(DbContextOptions<AnagamSolverWebAppContext> options)
			: base(options)
		{
		}

		public DbSet<AnagamSolverWebApp.Models.AnagramWordsModel> AnagramWords { get; set; } = default!;
	}
}
