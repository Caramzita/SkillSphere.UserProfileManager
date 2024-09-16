using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class UserProfileCfg : IEntityTypeConfiguration<UserProfileEntity>
{
    public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.HasIndex(e => e.UserId)
            .IsUnique();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ProfilePictureUrl)
            .HasMaxLength(500);

        builder.Property(e => e.Bio)
            .HasMaxLength(1000);

        builder.HasMany(e => e.Skills)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Goals)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.LearningHistories)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
