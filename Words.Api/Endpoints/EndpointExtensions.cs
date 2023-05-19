using Words.Api.Endpoints.Text;

namespace Words.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapTextEndpoints();

        return app;
    }
    
    private static IEndpointRouteBuilder MapTextEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPostText();
        app.MapDeleteAllWords();

        return app;
    }
}
