using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customer");

        builder
            .HasKey(x => x.Id)
            .IsClustered();

        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasOne(x => x.ShippingAddress)
            .WithOne();

        builder
            .HasOne(x => x.BillingAddress)
            .WithOne();

    }
}
