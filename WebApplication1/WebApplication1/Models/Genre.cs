namespace WebApplication1.Models;

public class Genre
{
    public int GenreId { get; set; }  
    public string Name { get; set; }
    public ICollection<BookGenre> GenreBooks { get; set; }  
}