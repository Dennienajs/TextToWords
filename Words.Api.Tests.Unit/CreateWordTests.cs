using Words.Api.Models;

namespace Words.Api.Tests.Unit;

public class UnitTest1
{
    [Theory]
    [InlineData("abcdefghijklmnopqrstuvwxyzæøå1234567890+´`¨^~*()_-{}[]:;@'#,./<>?|\\!\"£$%&")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ1234567890+´`¨^~*()_-{}[]:;@'#,./<>?|\\!\"£$%&")]
    public void Test1(string text)
    {
        Word word = Word.Create(text);
        
        Assert.Equal("abcdefghijklmnopqrstuvwxyzæøå", word.Entry);
    }
}
