using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable("Order");

        builder
            .HasKey(x => x.Id)
            .IsClustered();

        builder
            .Property(x => x.OrderDateTimeUtc)
            .IsRequired();

        builder
            .HasMany(x => x.Products)
            .WithMany()
            .UsingEntity<OrderProduct>();

        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .HasPrincipalKey(x => x.Id);
    }
}
