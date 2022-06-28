namespace Library.Api.TexlyzerEngine;

public class TexlyzerResult
{
    /// <summary>
    /// Represents Texlyzer Engine result.
    /// </summary>
    /// <param name="name">Book's title</param>
    /// <param name="uniqueId">Book's unique id.</param>
    public TexlyzerResult(string name, string uniqueId)
    {
        Name = name;
        UniqueId = uniqueId;
    }

    /// <summary>
    /// Books Unique ID
    /// </summary>
    public string UniqueId { get; set; }
    
    /// <summary>
    /// Books name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Word counts for the book.
    /// </summary>
    public IDictionary<string, int> WordCounts { get; set; } = new Dictionary<string, int>();
}