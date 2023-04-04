using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

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

    }
}
