using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class SkillCategoryCfg : IEntityTypeConfiguration<SkillCategory>
{
    public void Configure(EntityTypeBuilder<SkillCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(category => category.Skills)
            .WithOne()
            .HasForeignKey(skill => skill.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new SkillCategory("Programming"),
            new SkillCategory("Design & Creative")
        );
    }
}
