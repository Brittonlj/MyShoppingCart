using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .ToTable("Address");
          
        builder
            .HasKey(x => x.Id)
            .IsClustered();

        builder
            .Property(x => x.Street)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.City)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.State)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(10);
    }
}
