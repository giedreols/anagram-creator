﻿// <auto-generated />
using System;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnagramSolver.EF.DbFirst.Migrations
{
    [DbContext(typeof(AnagramSolverDataContext))]
    [Migration("20230708134306_AddOrderedFormToWordTable")]
    partial class AddOrderedFormToWordTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.Anagram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SearchWord")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Anagrams__3214EC0765F6E4AE");

                    b.HasIndex("WordId");

                    b.ToTable("Anagrams");
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.PartsOfSpeech", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullWord")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__PartsOfS__3214EC07BCBFAD36");

                    b.ToTable("PartsOfSpeech", (string)null);
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.SearchLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SearchWord")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__SearchLo__3214EC07CCA9F8D2");

                    b.ToTable("SearchLog", (string)null);
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MainForm")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OtherForm")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PartOfSpeechId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Words__3214EC0769E8B154");

                    b.HasIndex("PartOfSpeechId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.Anagram", b =>
                {
                    b.HasOne("AnagramSolver.EF.DbFirst.Entities.Word", "Word")
                        .WithMany("Anagrams")
                        .HasForeignKey("WordId")
                        .HasConstraintName("FK__Anagrams__WordId__05D8E0BE");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.Word", b =>
                {
                    b.HasOne("AnagramSolver.EF.DbFirst.Entities.PartsOfSpeech", "PartOfSpeech")
                        .WithMany("Words")
                        .HasForeignKey("PartOfSpeechId")
                        .HasConstraintName("FK__Words__AnagramId__60A75C0F");

                    b.Navigation("PartOfSpeech");
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.PartsOfSpeech", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("AnagramSolver.EF.DbFirst.Entities.Word", b =>
                {
                    b.Navigation("Anagrams");
                });
#pragma warning restore 612, 618
        }
    }
}
