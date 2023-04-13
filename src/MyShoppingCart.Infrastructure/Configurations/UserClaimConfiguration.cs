using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder
            .HasData(
                new IdentityUserClaim<Guid>
                {
                    Id = 1,
                    UserId = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                    ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    ClaimValue = "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 2,
                    UserId = new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                    ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    ClaimValue = "79F42C77-83E5-403B-9EC1-6A3FF285C5AC"
                }
            );
    }
}
