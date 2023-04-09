using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Application.Configuration;
using MyShoppingCart.Domain.Entities;
using System.Security.Claims;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class SecurityClaimConfiguration : IEntityTypeConfiguration<SecurityClaim>
{
    public void Configure(EntityTypeBuilder<SecurityClaim> builder)
    {
        builder
            .ToTable("Claim");

        builder
            .HasKey(x => x.Id)
            .IsClustered();

        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasData(
            new SecurityClaim(
                new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                ClaimTypes.NameIdentifier,
                "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
            new SecurityClaim(
                new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                ClaimTypes.Role,
                Roles.Customer),
            new SecurityClaim(
                new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                ClaimTypes.NameIdentifier,
                "79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
            new SecurityClaim(
                new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                ClaimTypes.Role,
                Roles.Admin));
    }
}
