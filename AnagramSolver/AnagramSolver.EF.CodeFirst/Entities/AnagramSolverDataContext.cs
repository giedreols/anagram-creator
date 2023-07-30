using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Entities;

public partial class AnagramSolverDataContext : DbContext
{
    public AnagramSolverDataContext()
    {
    }

    public AnagramSolverDataContext(DbContextOptions<AnagramSolverDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PartOfSpeechEntity> PartsOfSpeech { get; set; }

    public virtual DbSet<SearchLogEntity> SearchLog { get; set; }

    public virtual DbSet<WordEntity> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.\\DESKTOP-SPUUH8D;Initial Catalog=AnagramSolverData;Integrated Security=True;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PartOfSpeechEntity>(entity =>
         {
             entity.HasKey(e => e.Id).HasName("PK__PartsOfS__3214EC07BCBFAD36");

             entity.ToTable("PartsOfSpeech");

             entity.Property(e => e.Abbreviation).HasMaxLength(50);
             entity.Property(e => e.FullWord).HasMaxLength(50);
         });

        modelBuilder.Entity<SearchLogEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SearchLo__3214EC07CCA9F8D2");

            entity.ToTable("SearchLog");

            entity.Property(e => e.SearchWord).HasMaxLength(50);
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserIp).HasMaxLength(50);
        });

        modelBuilder.Entity<WordEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Words__3214EC0769E8B154");

            entity.Property(e => e.MainForm).HasMaxLength(50);
            entity.Property(e => e.OtherForm).HasMaxLength(50);
            entity.Property(e => e.OrderedForm).HasMaxLength(50);

            entity.HasOne(d => d.PartOfSpeech).WithMany(p => p.Words)
                .HasForeignKey(d => d.PartOfSpeechId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
