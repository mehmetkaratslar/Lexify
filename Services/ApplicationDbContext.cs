using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Lexify.Models;
using System.IO;

namespace Lexify.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Example> Examples { get; set; }
        public DbSet<WordRelation> WordRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Projenin yürütme dizinini al
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Veriler klasörünü oluştur (yoksa)
            string dataDirectory = Path.Combine(baseDirectory, "Veriler");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            // Veritabanı dosya yolunu belirle
            string dbPath = Path.Combine(dataDirectory, "LexifyDatabase.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kelime ilişkilerini yapılandırma
            modelBuilder.Entity<WordRelation>()
                .HasOne(wr => wr.Word)
                .WithMany(w => w.RelatedWordsFrom)
                .HasForeignKey(wr => wr.WordID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WordRelation>()
                .HasOne(wr => wr.RelatedWord)
                .WithMany(w => w.RelatedWordsTo)
                .HasForeignKey(wr => wr.RelatedWordID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}