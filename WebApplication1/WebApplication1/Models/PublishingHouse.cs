namespace WebApplication1.Models;

public class PublishingHouse
{
    public int PublishingHouseId { get; set; }  
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public ICollection<Book> Books { get; set; }  
}