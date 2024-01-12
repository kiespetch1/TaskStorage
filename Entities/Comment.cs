using Newtonsoft.Json;

namespace TaskStorage.Entities;

public class Comment
{
    public string? Text { get; set; }

    [JsonProperty("author.login")]
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