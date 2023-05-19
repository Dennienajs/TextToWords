using Microsoft.EntityFrameworkCore;
using Words.Api.Data;
using Words.Api.Models;

namespace Words.Api.Services;

public interface IWordsService
{
    List<Word> GetDistinctUniqueWords(string text);
    Task<int> AddRange(IEnumerable<Word> words, CancellationToken cancellationToken);
    Task<List<string>> GetWatchlistWordsFrom(IEnumerable<Word> words, CancellationToken cancellationToken);
    Task DeleteAllWords(CancellationToken cancellationToken);
}

public class WordsService : IWordsService
{
    private readonly WordsDbContext _context;
    
    public WordsService(WordsDbContext context)
    {
        _context = context;
    }

    public List<Word> GetDistinctUniqueWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new();
        
        List<Word> words = text
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(Word.Create)
            .DistinctBy(x => x.Entry)
            .ToList();
        
        return words;
    }

    public async Task<int> AddRange(IEnumerable<Word> words, CancellationToken cancellationToken)
    {
        List<Word> wordsNotInDb = words
            .Where(newWord => !_context.Words.Any(db => db.Entry == newWord.Entry))
            .ToList();
        
        _context.Words.AddRange(wordsNotInDb);
        await _context.SaveChangesAsync(cancellationToken);

        return wordsNotInDb.Count;
    }
    
    public async Task<List<string>> GetWatchlistWordsFrom(IEnumerable<Word> words, CancellationToken cancellationToken)
    {
        var wordEntries = words.Select(x => x.Entry);

        List<string> watchlistWords = await _context.Watchlist
            .AsNoTracking()
            .Select(x => x.Word)
            .Where(x => wordEntries.Contains(x))
            .ToListAsync(cancellationToken: cancellationToken);
        
        return watchlistWords;
    }
    
    public async Task DeleteAllWords(CancellationToken cancellationToken)
    {
        await _context.Words.ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
