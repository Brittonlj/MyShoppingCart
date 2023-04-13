using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder
            .HasData(
                new IdentityRole<Guid>
                {
                    Id = new Guid("28CA8CE6-CF1D-42B5-DA12-08DB3BA8F22D"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("357B00A9-EB69-4632-DA13-08DB3BA8F22D"),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );
    }
}
