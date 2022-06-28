namespace Library.Api.Configuration;

public class LibraryOptions
{
    /// <summary>
    /// Name of section in appsettings.json.
    /// </summary>
    public const string SectionName = "Library";

    /// <summary>
    /// List of books full file paths.
    /// </summary>
    public IList<string> Books { get; } = new List<string>();

    /// <summary>
    /// Default limit for number of words to return.
    /// </summary>
    public int DefaultApiResultLimit { get; set; } = 10;
}