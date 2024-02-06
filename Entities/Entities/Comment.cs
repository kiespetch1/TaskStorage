namespace TaskStorage.Entities;

/// <summary>
/// Представляет комментарий к задаче.
/// </summary>
public class Comment
{
    public string? Text { get; set; }
    
    public string Author { get; set; }

    public Comment(string text, string author)
    {
        Text = text;
        Author = author;
    }

    public Comment()
    {
    }
}