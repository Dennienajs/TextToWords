using Words.Api.Models;
using Words.Api.Services;

namespace Words.Api.Endpoints.Text;

public static class PostTextEndpoint
{
    public const string Name = "InsertDistinctUniqueWordsFromText";

    public static IEndpointRouteBuilder MapPostText(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Text.Post, async (
                TextRequest request,
                IWordsService wordsService,
                CancellationToken cancellationToken) =>
            {
                if (string.IsNullOrWhiteSpace(request.Text))
                    return TypedResults.Ok(new TextResponse(0, Enumerable.Empty<string>()));
                
                List<Word> words = wordsService.GetDistinctUniqueWords(request.Text);
                
                int wordsAdded = await wordsService.AddRange(words, cancellationToken);
                List<string> watchlistWords = await wordsService.GetWatchlistWordsFrom(words, cancellationToken);
                
                TextResponse response = new(wordsAdded, watchlistWords);
                
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Accepts<TextRequest>("application/json")
            .Produces<TextResponse>();

        return app;
    }
}

public record TextRequest(string Text);
public record TextResponse(int DistinctUniqueWords, IEnumerable<string> WatchlistWords);
