namespace TaskStorage.Entities.Models;

/// <summary>
/// Представляет данные кастомных полей.
/// </summary>
public class CustomFieldInfo
{
    public string Name { get; set; }
    
    public Value Value { get; set; }

    public CustomFieldInfo()
    {
    }
}