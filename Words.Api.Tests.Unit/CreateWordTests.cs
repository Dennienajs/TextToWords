using FluentAssertions;
using Words.Api.Models;

namespace Words.Api.Tests.Unit;

public class CreateWordTests
{
    [Theory]
    [InlineData("abcdefghijklmnopqrstuvwxyzæøå1234567890+´`¨^~*()_-{}[]:;@'#,./<>?|\\!\"£$%&", "abcdefghijklmnopqrstuvwxyzæøå")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ 1234567890+´`¨^~*()_-{}[]:;@'#,./<>?|\\!\"£$%&", "abcdefghijklmnopqrstuvwxyzæøå")]
    [InlineData("HelloWorld!", "helloworld")]
    [InlineData("Hello@World..", "helloworld")]
    [InlineData("", "")]
    [InlineData("123", "")]
    public void CreateWord_Should_ReturnWordWithEntryOnlyContainingLowerCaseLetters(string text, string expected)
    {
        // Act
        Word word = Word.Create(text);
        
        // Assert
        word.Entry.Should().Be(expected);
    }
}
