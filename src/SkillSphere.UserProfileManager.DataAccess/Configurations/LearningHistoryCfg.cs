using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class LearningHistoryCfg : IEntityTypeConfiguration<LearningHistoryEntity>
{
    public void Configure(EntityTypeBuilder<LearningHistoryEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();      

        builder.Property(x => x.CourseTitle)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.CourseTitle)
            .IsUnique();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);
    }
}
