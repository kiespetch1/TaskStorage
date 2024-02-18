using TaskStorage.Entities;
using TaskStorage.Entities.Enums;
using TaskStorage.Entities.Models;
using Type = TaskStorage.Entities.Models.Type;

namespace TaskStorage.Utils;
/// <summary>
/// Метод для маппинга полей.
/// </summary>
public static class FieldsMapping
{
    public static Issue AddCustomParameters(this Issue issue, List<CustomFieldInfo> customFieldsList)
    {
        for (var i = 0; i <  customFieldsList.Count - 1 ; i++)
        {
            switch (customFieldsList[i].Name)
            {
                case "Priority":
                    issue.Priority = (Priority)customFieldsList[i].Value.Ordinal;
                    break;
                case "State":
                    issue.State = (State)customFieldsList[i].Value.Ordinal;
                    break;
                case "Type":
                    issue.Type = (Type)customFieldsList[i].Value.Ordinal;
                    break;
                case "Assignee":
                    issue.Assignee = new Assignee { Login = customFieldsList[i].Value.Login  };
                    break;
                case "Spent time":
                    issue.SpentTime = new TimeSpan(0, 0, customFieldsList[i].Value.Minutes.GetValueOrDefault());
                    break;
            }
        }

        return issue;
    }

    public static Issue AddWorkLogs(this Issue issue, List<WorkLogInfo> workLogList)
    {
        issue.WorkLogs = workLogList;
        return issue;
    }
}