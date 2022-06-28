using Library.Api.Repositories;

namespace Library.Api.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;

    public BooksService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<IDictionary<string, string>> GetTittles()
    {
        return await _booksRepository.GetBookTittles();
    }

    public async Task<IDictionary<string, int>?> GetTopWords(string bookId, string? query, int? limit)
    {
        return await _booksRepository.GetTopWords(bookId, query, limit);
    }
}