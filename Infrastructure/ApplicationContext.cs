using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using TaskStorage.Entities;
using TaskStorage.Utils;

namespace TaskStorage;

public class ApplicationContext : DbContext, IDatabaseContext
{
    public DbSet<Issue> Issues { get; set; }

    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Issue>().ToCollection("issues");
    }
}