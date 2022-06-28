using System.Text.RegularExpressions;

namespace Library.Api.TexlyzerEngine;

/// <summary>
/// Text Analyzer Engine
/// </summary>
public class TexlyzerEngine : ITexlyzerEngine
{
    private readonly Regex _wordMatcher = new Regex("[a-z]{5,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public async Task<TexlyzerResult> ProcessFile(string filePath)
    {
        var title = Path.GetFileNameWithoutExtension(filePath);
        // Very basic ID generator.
        var uniqueId = Math.Abs(title.GetHashCode()).ToString();
        var result = new TexlyzerResult(title, uniqueId);
        var fileContent = await File.ReadAllTextAsync(filePath);
        var wordMatches = _wordMatcher.Matches(fileContent);
        var wordsDictionary = new Dictionary<string, int>();
        foreach (Match match in wordMatches)
        {
            var word = match.Value.ToLower();
            if (wordsDictionary.ContainsKey(word))
                wordsDictionary[word]++;
            else wordsDictionary.Add(word, 1);
        }
        result.WordCounts = wordsDictionary.OrderByDescending(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);

        return result;
    }
}