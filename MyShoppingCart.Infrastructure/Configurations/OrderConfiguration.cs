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
            .WithMany(x => x.Orders)
            .UsingEntity<OrderProduct>();
    }
}
