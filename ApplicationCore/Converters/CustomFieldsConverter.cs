using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskStorage.Entities.Models;

namespace TaskStorage.Converters;

/// <summary>
/// Преобразует данные о комментариях из JSON.
/// </summary>
public class CustomFieldsConverter : JsonConverter
{
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var customFields = obj["customFields"];
        var list = new List<CustomFieldInfo>();

        foreach (var field in customFields)
        {
            int? ordinal = null;
            string? login = null;
            int? minutes = null;

            var name = field?["name"]?.ToString();

            if (field?["value"].HasValues == true)
            {
                switch (field?["value"]?["$type"].ToString())
                {
                    case "User":
                        login = field?["value"]?["login"].ToString();
                        break;
                    case "PeriodValue":
                        minutes = Convert.ToInt32(field?["value"]?["minutes"].ToString());
                        break;
                    default:
                        ordinal = Convert.ToInt32(field?["value"]?["ordinal"].ToString());
                        break;
                }
            }
            
            list.Add(new CustomFieldInfo
            {
                Name = name,
                Value = new Value
                {
                    Ordinal = ordinal,
                    Login = login,
                    Minutes = minutes
                }
            });
        }
        
        return list;
    }

    public override bool CanConvert(System.Type objectType)
    {
        return objectType == typeof(List<CustomFieldInfo>);
    }
}