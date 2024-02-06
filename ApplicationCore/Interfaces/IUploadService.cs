using TaskStorage.Entities;
using TaskStorage.Entities.Models;

namespace TaskStorage.Interfaces;

/// <summary>
/// Определяет методы выгрузки задач с YouTrack.
/// </summary>
public interface IUploadService
{
    /// <summary>
    /// Выгружает с YouTrack все задачи.
    /// </summary>
    /// <returns>Список задач.</returns>
    public Task<List<Issue>> UploadAll();

    /// <summary>
    /// Парсит данные JSON в модель.
    /// </summary>
    /// <param name="idList">Список id необходимых задач.</param>
    /// <returns>Список задач.</returns>
    public Task<List<Issue>> ParseIssues(List<IssueIdData> idList);

    /// <summary>
    /// Выгружает с YouTrack только новые задачи.
    /// </summary>
    /// <returns>Список задач.</returns>
    public Task<List<Issue>> UploadNew();
}