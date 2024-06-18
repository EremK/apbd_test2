using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class BookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDTO>> GetAllBooksAsync(DateTime? releaseDate = null)
    {
        var query = _context.Book
            .Include(b => b.PublishingHouse)
            .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
            .Include(b => b.GenreBooks).ThenInclude(gb => gb.Genre)
            .AsQueryable(); 
        
        if (releaseDate.HasValue)
        {
            query = query.Where(b => b.ReleaseDate.Date == releaseDate.Value.Date);
        }

        return await query.Select(b => new BookDTO
            {
                Title = b.Name,
                ReleaseDate = b.ReleaseDate,
                PublishingHouseName = b.PublishingHouse.Name,
                Authors = b.AuthorBooks.Select(ab => ab.Author.Name).ToList(),
                Genres = b.GenreBooks.Select(gb => gb.Genre.Name).ToList()
            })
            .OrderByDescending(b => b.ReleaseDate)
            .ThenBy(b => b.PublishingHouseName)
            .ToListAsync();
    }
    
    public async Task AddBookAsync(CreateBookDTO newBook)
    {
        var book = new Book
        {
            Name = newBook.Title,
            ReleaseDate = newBook.ReleaseDate,
            PublishingHouseId = newBook.PublishingHouseId
        };
        
        if (!_context.PublishingHouse.Any(ph => ph.PublishingHouseId == newBook.PublishingHouseId))
        {
            throw new ArgumentException("PublishingHouseId not found.");
        }

        var authorIds = await _context.Author.Where(a => newBook.AuthorIds.Contains(a.AuthorId)).Select(a => a.AuthorId).ToListAsync();
        foreach (var authorId in newBook.AuthorIds)
        {
            if (!authorIds.Contains(authorId))
            {
                throw new ArgumentException($"AuthorId {authorId} not found.");
            }
            book.AuthorBooks.Add(new BookAuthor { AuthorId = authorId, Book = book });
        }

        var genreIds = await _context.Genre.Where(g => newBook.GenreIds.Contains(g.GenreId)).Select(g => g.GenreId).ToListAsync();
        foreach (var genreId in newBook.GenreIds)
        {
            if (!genreIds.Contains(genreId))
            {
                throw new ArgumentException($"GenreId {genreId} not found.");
            }
            book.GenreBooks.Add(new BookGenre { GenreId = genreId, Book = book });
        }

        _context.Book.Add(book);
        await _context.SaveChangesAsync();
    }
}
