namespace TaskStorage.Utils;
/// <summary>
/// Класс с глобальными переменными.
/// </summary>
public class GlobalVariables
{
    internal static DateTime LastDbUpdateTime { get; set; } = new(1,1,1);
}