using TaskStorage.Entities;

namespace TaskStorage.Interfaces;

/// <summary>
/// Определяет методы работы с БД.
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Добавляет данные о задачах в БД.
    /// </summary>
    /// <param name="issues">Список добавляемых задач.</param>
    public Task InsertManyAsync(List<Issue> issues);

    /// <summary>
    /// Добавляет или обновляет данные о задаче  в БД.
    /// </summary>
    /// <param name="issue">Обновляемая задача.</param>
    public Task CreateOrUpdateAsync(Issue issue);
}