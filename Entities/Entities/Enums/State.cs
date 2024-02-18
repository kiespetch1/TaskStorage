namespace TaskStorage.Entities.Enums;

/// <summary>
/// Представляет статус задачи.
/// </summary>
public enum State
{
    ToDo,
    Open,
    InProgress,
    ToBeDiscussed,
    Reopened,
    CantReproduce,
    Duplicate,
    OnReview,
    WontFix,
    Incomplete,
    Obsolete,
    Verified,
    ToTest,
    Testing,
    BugsFound,
    Done,
    Released,
    Cancelled
}