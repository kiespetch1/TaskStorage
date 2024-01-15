namespace Entities.Entities;

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