using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class UserSkillCfg : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(us => new { us.UserId, us.SkillId });

        builder.Property(us => us.Level)
            .HasConversion<string>();

        builder.HasOne<UserProfile>()
            .WithMany(up => up.Skills)
            .HasForeignKey(us => us.UserId)
            .HasPrincipalKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.Skill)
            .WithMany()
            .HasForeignKey(us => us.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
