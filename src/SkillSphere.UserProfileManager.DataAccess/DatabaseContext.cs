using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;
using System.Reflection;

namespace SkillSphere.UserProfileManager.DataAccess;

public class DatabaseContext : DbContext
{
    private readonly ILogger<DatabaseContext> _logger;

    public DbSet<UserProfileEntity> UserProfiles { get; set; }

    public DbSet<SkillEntity> Skills { get; set; }

    public DbSet<GoalEntity> Goals { get; set; }

    public DbSet<LearningHistoryEntity> LearningHistory { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _logger.LogWarning("DbContextOptionsBuilder is not configured.");
        }

        optionsBuilder.EnableSensitiveDataLogging()
                      .LogTo(log => _logger.LogInformation(log));
    }
}
