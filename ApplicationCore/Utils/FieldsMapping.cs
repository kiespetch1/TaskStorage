using Entities.Entities;
using Entities.Entities.DTOs;
using Type = Entities.Entities.DTOs.Type;

namespace ApplicationCore.Utils;

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
}