using Library.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    /// <summary>
    /// Returns book's titles and their Ids.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "Get Book Titles")]
    public async Task<IDictionary<string, string>> Get()
    {
        return await _booksService.GetTittles();
    }

    /// <summary>
    /// Returns top words for given book id.
    /// </summary>
    /// <param name="id">Book Id</param>
    /// <param name="query">Search phrase to filter words with start-with type query.</param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "Get Top 10 Words")]
    public async Task<IDictionary<string, int>?> GetBookTop10Words(string id, string? query)
    {
        return await _booksService.GetTopWords(id, query, 10);
    }
}