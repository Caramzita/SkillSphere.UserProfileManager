using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class GoalCfg : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.Title)
            .IsUnique();

        builder.Property(p => p.Progress)
            .IsRequired()
            .HasConversion<string>();
    }
}
