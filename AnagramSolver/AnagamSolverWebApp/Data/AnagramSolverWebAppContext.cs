using AnagramSolverWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolverWebApp.Data
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
