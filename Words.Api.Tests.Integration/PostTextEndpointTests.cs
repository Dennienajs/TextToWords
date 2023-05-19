using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Words.Api.Endpoints.Text;

namespace Words.Api.Tests.Integration;

public class PostTextEndpointTests 
    : IAsyncLifetime, IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public PostTextEndpointTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }
    
    public async Task InitializeAsync()
    {
        // Called before each test
        await DeleteAllWords();
    }
    public async Task DisposeAsync()
    {
        // Called after each test
        await DeleteAllWords();
    }
    
    // Normally I would not just delete all words in the database but, considering the scope of this project, I think it's fine for now.
    private async Task DeleteAllWords() => await _httpClient.DeleteAsync(ApiEndpoints.Text.DeleteAllWords);

    
    [Fact]
    public async Task Post_ReturnsDistinctUniqueWords_Then_NoDistinctUniqueWordsWhenAlreadyInDatabase()
    {
        // Arrange
        TextRequest request = new("a horse and a dog");
        
        // Act
        var response1 = await _httpClient.PostAsJsonAsync(ApiEndpoints.Text.Post, request);
        var response2 = await _httpClient.PostAsJsonAsync(ApiEndpoints.Text.Post, request);
        
        // Assert
        response1.EnsureSuccessStatusCode();
        response2.EnsureSuccessStatusCode();
        
        var textResponse1 = await response1.Content.ReadFromJsonAsync<TextResponse>();
        var textResponse2 = await response2.Content.ReadFromJsonAsync<TextResponse>();
        
        textResponse1!.DistinctUniqueWords.Should().Be(4); // a, horse, and, dog
        textResponse2!.DistinctUniqueWords.Should().Be(0); // all words already in database
        
        textResponse1.WatchlistWords.Should().BeEquivalentTo("horse", "dog");
        textResponse1.WatchlistWords.Should().BeEquivalentTo("horse", "dog");
    }
}
