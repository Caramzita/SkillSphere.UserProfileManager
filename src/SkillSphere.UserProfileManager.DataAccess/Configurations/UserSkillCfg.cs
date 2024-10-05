using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class UserSkillCfg : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(us => new { us.UserId, us.SkillId });

        builder.HasOne(us => us.UserProfile)
            .WithMany(u => u.Skills)
            .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Skill)
            .WithMany()
            .HasForeignKey(us => us.SkillId);
    }
}
