using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskStorage.Entities.Models;

/// <summary>
/// Представляет информацию о выполненной работе.
/// </summary>
public class WorkLogInfo
{
    public int Duration { get; set; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
}

