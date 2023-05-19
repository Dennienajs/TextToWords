using Microsoft.EntityFrameworkCore;
using Words.Api.Models;

namespace Words.Api.Data;

public class WordsDbContext : DbContext
{
    public WordsDbContext(DbContextOptions<WordsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureWord(modelBuilder);
        ConfigureWatchlist(modelBuilder);
        
        modelBuilder
            .Entity<Watchlist>()
            .HasData(new List<Watchlist>
            {
                new() { Id = 1, Word = "horse" },
                new() { Id = 2, Word = "zebra" },
                new() { Id = 3, Word = "dog" },
                new() { Id = 4, Word = "elephant" }
            });
    }

    
    public DbSet<Word> Words => Set<Word>();
    public DbSet<Watchlist> Watchlist => Set<Watchlist>();
    
    
    private static void ConfigureWord(ModelBuilder modelBuilder)
    {

        modelBuilder
            .Entity<Word>()
            .ToTable("Words")
            .HasKey(w => w.Id);

        modelBuilder.Entity<Word>()
            .HasIndex(x => x.Entry)
            .IsUnique();

        modelBuilder.Entity<Word>()
            .Property(x => x.Entry)
            .HasMaxLength(255);
    }
    private static void ConfigureWatchlist(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Watchlist>()
            .ToTable("Watchlist")
            .HasKey(w => w.Id);

        modelBuilder
            .Entity<Watchlist>()
            .HasIndex(x => x.Word)
            .IsUnique();
        
        modelBuilder.Entity<Watchlist>()
            .Property(x => x.Word)
            .HasMaxLength(255);
    }
}
