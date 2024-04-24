using FonTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FonTech.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);

        builder.HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "User"
            },
            new()
            {
                Id = 2,
                Name = "Admin"
            },
            new()
            {
                Id = 3,
                Name = "Moderator"
            },
        });
    }
}
