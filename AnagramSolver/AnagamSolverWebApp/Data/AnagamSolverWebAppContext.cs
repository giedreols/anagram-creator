using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnagamSolverWebApp.Models;

namespace AnagamSolverWebApp.Data
{
    public class AnagamSolverWebAppContext : DbContext
    {
        public AnagamSolverWebAppContext (DbContextOptions<AnagamSolverWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AnagamSolverWebApp.Models.AnagramWordsModel> AnagramWords { get; set; } = default!;
    }
}
