namespace Words.Api.Tests.Integration;

public class UnitTest1 : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    
    private readonly HttpClient _httpClient;

    public UnitTest1(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }
    
    [Fact]
    public void Test1()
    {

    }
}
