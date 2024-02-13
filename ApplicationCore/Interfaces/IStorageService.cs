
namespace TaskStorage.Interfaces;

/// <summary>
/// Определяет методы выгрузки задач в базу данных.
/// </summary>
public interface IStorageService 
{
    /// <summary>
    /// Выгружает только новые задачи в базу данных.
    /// </summary>
    /// <returns></returns>
    public Task StoreNewIssues();
}