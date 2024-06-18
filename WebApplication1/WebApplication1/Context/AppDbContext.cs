using WebApplication1.Models;

namespace WebApplication1.Context;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<PublishingHouse> PublishingHouse { get; set; }
    public DbSet<BookAuthor> BookAuthor { get; set; }
    public DbSet<BookGenre> BookGenre { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(b => b.BookId);
        modelBuilder.Entity<Author>()
            .HasKey(a => a.AuthorId);
        modelBuilder.Entity<Genre>()
            .HasKey(g => g.GenreId);
        modelBuilder.Entity<PublishingHouse>()
            .HasKey(p => p.PublishingHouseId);

        modelBuilder.Entity<BookAuthor>()
            .HasKey(ab => new { ab.AuthorId, ab.BookId });
        modelBuilder.Entity<BookGenre>()
            .HasKey(gb => new { gb.GenreId, gb.BookId });

        modelBuilder.Entity<Book>()
            .HasOne(b => b.PublishingHouse)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublishingHouseId);
    }
}

