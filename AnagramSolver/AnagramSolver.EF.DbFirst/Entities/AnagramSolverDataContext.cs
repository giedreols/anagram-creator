﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DbFirst.Entities;

public partial class AnagramSolverDataContext : DbContext
{
	public AnagramSolverDataContext()
    {
    }

    public AnagramSolverDataContext(DbContextOptions<AnagramSolverDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anagram> Anagrams { get; set; }

    public virtual DbSet<PartsOfSpeech> PartsOfSpeech{ get; set; }

    public virtual DbSet<SearchLog> SearchLog { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True;TrustServerCertificate=true");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anagram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Anagrams__3214EC0765F6E4AE");

            entity.Property(e => e.SearchWord).HasMaxLength(50);
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Word).WithMany(p => p.Anagrams)
                .HasForeignKey(d => d.WordId)
                .HasConstraintName("FK__Anagrams__WordId__05D8E0BE");
        });

        modelBuilder.Entity<PartsOfSpeech>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartsOfS__3214EC07BCBFAD36");

            entity.ToTable("PartsOfSpeech");

            entity.Property(e => e.Abbreviation).HasMaxLength(50);
            entity.Property(e => e.FullWord).HasMaxLength(50);
        });

        modelBuilder.Entity<SearchLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SearchLo__3214EC07CCA9F8D2");

            entity.ToTable("SearchLog");

            entity.Property(e => e.SearchWord).HasMaxLength(50);
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserIp).HasMaxLength(50);
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Words__3214EC0769E8B154");

            entity.Property(e => e.MainForm).HasMaxLength(50);
            entity.Property(e => e.OtherForm).HasMaxLength(50);

            entity.HasOne(d => d.PartOfSpeech).WithMany(p => p.Words)
                .HasForeignKey(d => d.PartOfSpeechId)
                .HasConstraintName("FK__Words__AnagramId__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
