using Microsoft.EntityFrameworkCore;
using TaskStorage.Entities;

namespace TaskStorage.Utils;

// <summary>
/// Database context class
/// <summary/>
public interface IDatabaseContext
{
    public DbSet<Issue> Issues { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}