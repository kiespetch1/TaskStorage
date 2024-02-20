using TaskStorage.Entities;

namespace TaskStorage.Interfaces;

/// <summary>
/// Определяет методы работы с БД.
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Добавляет данные о задаче в БД.
    /// </summary>
    /// <param name="newIssue">Добавляемая задача.</param>
    public Task CreateAsync(Issue newIssue);

    /// <summary>
    /// Обновляет данные о задаче в БД.
    /// </summary>
    /// <param name="updatedIssue">Обновляемая задача.</param>
    public Task UpdateAsync(Issue updatedIssue);
}