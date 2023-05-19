using Microsoft.AspNetCore.Http.HttpResults;
using Words.Api.Services;

namespace Words.Api.Endpoints.Text;

public static class DeleteAllWordsEndpoint
{
    public const string Name = "DeleteAllWords";

    public static IEndpointRouteBuilder MapDeleteAllWords(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Text.DeleteAllWords, async (
                IWordsService wordsService,
                CancellationToken cancellationToken) =>
            {
                await wordsService.DeleteAllWords(cancellationToken);
                
                return TypedResults.NoContent();
            })
            .WithName(Name)
            .Produces<NoContent>();

        return app;
    }
}
