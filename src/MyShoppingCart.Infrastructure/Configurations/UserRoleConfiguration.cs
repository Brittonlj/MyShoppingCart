using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder
            .HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = new Guid("28CA8CE6-CF1D-42B5-DA12-08DB3BA8F22D"),
                    UserId = new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC")
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = new Guid("357B00A9-EB69-4632-DA13-08DB3BA8F22D"),
                    UserId = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5")
                }
            );
    }
}
