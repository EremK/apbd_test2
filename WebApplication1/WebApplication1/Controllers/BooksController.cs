using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] DateTime? releaseDate)
    {
        var books = await _bookService.GetAllBooksAsync(releaseDate);
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO bookDto)
    {
        try
        {
            await _bookService.AddBookAsync(bookDto);
            return StatusCode(201); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
}
