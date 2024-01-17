namespace Entities.Entities.DTOs;

public class WorkLogInfo
{
    public int Duration { get; set; }
    public DateOnly Date { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
}

