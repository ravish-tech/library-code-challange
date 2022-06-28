using System.Collections;
using Library.Api.Configuration;
using Library.Api.TexlyzerEngine;
using Microsoft.Extensions.Options;

namespace Library.Api.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IDictionary<string, string> _books = new Dictionary<string, string>();

    private readonly IDictionary<string, IDictionary<string, int>> _wordCounts =
        new Dictionary<string, IDictionary<string, int>>();

    private readonly LibraryOptions _libraryOptions;

    public BooksRepository(ITexlyzerEngine texlyzerEngine, IOptions<LibraryOptions> configuration)
    {
        _libraryOptions = configuration.Value;
        foreach (var bookFile in configuration.Value.Books)
        {
            var result = texlyzerEngine.ProcessFile(bookFile).Result;
            _books.Add(result.UniqueId, result.Name);
            _wordCounts.Add(result.UniqueId, result.WordCounts);
        }
    }

    public Task<IDictionary<string, string>> GetBookTittles()
    {
        return Task.FromResult(_books);
    }

    private static IDictionary<string, int> EnumerableToDictionary(IEnumerable<KeyValuePair<string, int>> result,
        int limit) => result.Take(limit).ToDictionary(x => x.Key, x => x.Value);

    public Task<IDictionary<string, int>?> GetTopWords(string bookId, string? query, int? limit)
    {
        // Note: Things like this slows down the execution, but are necessary for external facing APIs.
        // In our example, our UI should bare the responsibility of cleaning the data before sending.
        // query = query?.Trim().ToLower();

        if (!_books.ContainsKey(bookId)) return Task.FromResult<IDictionary<string, int>?>(null);

        IEnumerable<KeyValuePair<string, int>> result = _wordCounts[bookId];

        if (query is { Length: > 2 })
        {
            result = _wordCounts[bookId].Where(word => word.Key.StartsWith(query, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(EnumerableToDictionary(result, limit ?? _libraryOptions.DefaultApiResultLimit))!;
    }
}