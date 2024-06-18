namespace WebApplication1.Models;

public class Book
{
    public int BookId { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PublishingHouseId { get; set; } 
    public PublishingHouse PublishingHouse { get; set; }  
    public ICollection<BookAuthor> AuthorBooks { get; set; }  
    public ICollection<BookGenre> GenreBooks { get; set; }  
}