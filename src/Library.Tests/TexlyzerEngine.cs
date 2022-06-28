using Library.Api.TexlyzerEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests;

[TestClass]
public class TexlyzerEngineTests
{
    private const string SampleText = "some random words with Some punctuation. Is not counted. '432$ Random";
    private const int TotalWords = 4;
    private const string MostRepeatedWord = "random";
    private const int MostRepeatedWordCount = 2;

    [TestMethod]
    public async Task Test_Process_File()
    {
        // Setup
        var filePath = Path.Join(Path.GetTempPath(), "bookTitle.txt");
        await using var fs = File.CreateText(filePath);
        await fs.WriteAsync(SampleText);
        await fs.FlushAsync();
        fs.Close();
        var engine = new TexlyzerEngine();

        // Act
        var result = await engine.ProcessFile(filePath);

        // Assert
        Assert.AreEqual("bookTitle", result.Name);
        Assert.AreEqual(TotalWords, result.WordCounts.Count);
        Assert.AreEqual(MostRepeatedWord, result.WordCounts.First().Key);
        Assert.AreEqual(MostRepeatedWordCount, result.WordCounts.First().Value);
    }
}