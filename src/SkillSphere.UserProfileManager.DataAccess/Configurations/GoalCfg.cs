using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class GoalCfg : IEntityTypeConfiguration<GoalEntity>
{
    public void Configure(EntityTypeBuilder<GoalEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.Title)
            .IsUnique();

        builder.Property(x => x.Progress).IsRequired();
    }
}
