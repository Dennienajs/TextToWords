using Microsoft.EntityFrameworkCore;
using Words.Api.Data;
using Words.Api.Models;

namespace Words.Api.Services;

public interface IWordsService
{
    List<Word> GetDistinctUniqueWords(string text);
    Task<int> AddRange(List<Word> words, CancellationToken cancellationToken);
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
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(Word.Create)
            .DistinctBy(x => x.Entry, StringComparer.OrdinalIgnoreCase)
            .ToList();
        
        return words;
    }
    
    public async Task<int> AddRange(List<Word> words, CancellationToken cancellationToken)
    {
        IEnumerable<string> newEntries = words.Select(x => x.Entry);
        
        List<string> wordEntriesAlreadyInDb = await _context.Words
            .AsNoTracking()
            .Where(x => newEntries.Contains(x.Entry))
            .Select(x => x.Entry)
            .ToListAsync(cancellationToken: cancellationToken);


        List<Word> wordsToAdd = words
            .ExceptBy(wordEntriesAlreadyInDb, x => x.Entry)
            .ToList();
        
        _context.Words.AddRange(wordsToAdd);
        await _context.SaveChangesAsync(cancellationToken);
        
        return wordsToAdd.Count;
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
