using FonTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FonTech.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Login).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired();

        builder
            .HasMany(u => u.Reports)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .HasPrincipalKey(u => u.Id);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>
            (
                entityTypeBuilder => entityTypeBuilder.HasOne<Role>().WithMany().HasForeignKey(ur => ur.RoleId),   
                entityTypeBuilder => entityTypeBuilder.HasOne<User>().WithMany().HasForeignKey(ur => ur.UserId)
            );
    }
}
