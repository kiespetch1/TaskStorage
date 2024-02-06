namespace TaskStorage.Entities.Models;

/// <summary>
/// Представляет информацию о выполненной работе.
/// </summary>
public class WorkLogInfo
{
    public int Duration { get; set; }
    public DateOnly Date { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
}

