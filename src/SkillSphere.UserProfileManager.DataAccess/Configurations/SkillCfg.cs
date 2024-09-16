using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Configurations;

public class SkillCfg : IEntityTypeConfiguration<SkillEntity>
{
    public void Configure(EntityTypeBuilder<SkillEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.Name);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.Property(x => x.Level).IsRequired();
    }
}
