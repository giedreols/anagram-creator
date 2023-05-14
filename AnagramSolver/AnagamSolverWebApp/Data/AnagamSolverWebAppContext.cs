using AnagramSolverWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolverWebApp.Data
{
	public class AnagamSolverWebAppContext : DbContext
	{
		public AnagamSolverWebAppContext(DbContextOptions<AnagamSolverWebAppContext> options)
			: base(options)
		{
		}

		public DbSet<AnagramWordsModel> AnagramWords { get; set; } = default!;
	}
}
