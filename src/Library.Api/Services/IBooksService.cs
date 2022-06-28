namespace Library.Api.Services;

public interface IBooksService
{
    /// <summary>
    /// Get books titles as list of strings.
    /// </summary>
    /// <returns>List of titles.</returns>
    public Task<IDictionary<string, string>> GetTittles();

    /// <summary>
    /// Get words for given book ordered by count ascending.
    /// </summary>
    /// <param name="bookId">Id of book to get word count for.</param>
    /// <param name="query">Query string to filter words. If not present all words are returned.</param>
    /// <param name="limit">Number of words to return.</param>
    /// <returns>List of words with their counts.</returns>
    public Task<IDictionary<string, int>?> GetTopWords(string bookId, string? query, int? limit);
}