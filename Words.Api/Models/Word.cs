namespace Words.Api.Models;

public sealed class Word
{
    public int Id { get; set; }
    public required string Entry { get; set; }
    
    public static Word Create(string entry)
    {
        string word = new(entry.ToLower().Where(char.IsLetter).ToArray());
        
        return new()
        {
            Entry = word
        };
    }
}