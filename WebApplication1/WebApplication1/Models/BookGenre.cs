namespace WebApplication1.Models;

public class BookGenre
{
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}