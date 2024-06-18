namespace WebApplication1.DTOs;

public class BookDTO
{
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string PublishingHouseName { get; set; }
    public List<string> Authors { get; set; }
    public List<string> Genres { get; set; }
}

public class CreateBookDTO
{
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PublishingHouseId { get; set; }
    public List<int> AuthorIds { get; set; }
    public List<int> GenreIds { get; set; }
}