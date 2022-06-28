using Library.Api.Configuration;
using Library.Api.Repositories;
using Library.Api.TexlyzerEngine;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Tests;

[TestClass]
public class BooksRepositoryTests
{
    private static readonly IDictionary<string, int> Words = new Dictionary<string, int>
    {
        { "word1", 123 },
        { "word2", 321 },
        { "post1", 22 },
        { "post2", 21 }
    };

    private static Mock<ITexlyzerEngine> GetMockedTexlyzerEngine()
    {
        var texlyzerEngineMock = new Mock<ITexlyzerEngine>();
        texlyzerEngineMock.Setup(engine => engine.ProcessFile(It.IsAny<string>())).Returns(() =>
        {
            var result = new TexlyzerResult("BookTitle", "12345");
            foreach (var word in Words)
                result.WordCounts.Add(word);
            return Task.FromResult(result);
        });
        return texlyzerEngineMock;
    }

    [TestMethod]
    public async Task Test_Get_Book_Titles()
    {
        // Setup
        var texlyzerEngineMock = GetMockedTexlyzerEngine();
        var libOptions = new LibraryOptions();
        libOptions.Books.Add("BookFile.txt");
        var options = Options.Create(libOptions);
        var repository = new BooksRepository(texlyzerEngineMock.Object, options);

        // Act
        var title = await repository.GetBookTittles();

        // Assert
        Assert.AreEqual("BookTitle", title.First().Value);
        Assert.AreEqual("12345", title.First().Key);
    }

    [TestMethod]
    [DataRow("", 0, 0)]
    [DataRow("", 10, 4)]
    [DataRow("wo", 10, 4)]
    [DataRow("wor", 10, 2)]
    [DataRow("post", 10, 2)]
    [DataRow("post", 1, 1)]
    [DataRow("post", 0, 0)]
    [DataRow("invalid", 10, 0)]
    public async Task Test_Get_Words_With_Query(string query, int limit, int expectedCount)
    {
        // Setup
        var texlyzerEngineMock = GetMockedTexlyzerEngine();
        var libOptions = new LibraryOptions();
        libOptions.Books.Add("BookFile.txt");
        var options = Options.Create(libOptions);
        var repository = new BooksRepository(texlyzerEngineMock.Object, options);

        // Act
        var words = await repository.GetTopWords("12345", query, limit);

        // Assert
        Assert.AreEqual(expectedCount, words?.Count);
        if (!string.IsNullOrWhiteSpace(query) && query.Length > 2 && words != null)
        {
            foreach (var word in words)
            {
                Assert.IsTrue(word.Key.StartsWith(query));
            }
        }
    }
}