namespace Library.Api.TexlyzerEngine;

public interface ITexlyzerEngine
{
    /// <summary>
    /// Processes file by reading its contents and returning book's name and word counts.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    Task<TexlyzerResult> ProcessFile(string filePath);
}