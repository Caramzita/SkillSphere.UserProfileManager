using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class LearningHistoryCfg : IEntityTypeConfiguration<LearningHistory>
{
    public void Configure(EntityTypeBuilder<LearningHistory> builder)
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

        builder.HasOne<UserProfile>()
            .WithMany(up => up.LearningHistories)
            .HasForeignKey(us => us.UserId)
            .HasPrincipalKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
