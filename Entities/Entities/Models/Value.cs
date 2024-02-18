namespace TaskStorage.Entities.Models;

/// <summary>
/// Представляет значения, получаемые из JSON.
/// </summary>
public class Value
{
    public Type Type { get; set; }

    public int? Ordinal { get; set; }

    public string? Login { get; set; }

    public int? Minutes { get; set; }
}