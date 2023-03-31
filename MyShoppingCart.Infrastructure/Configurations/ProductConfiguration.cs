using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Product");

        builder
            .HasKey(x => x.Id)
            .IsClustered();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .Property(x => x.ImageUrl)
            .HasMaxLength(50);

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasPrecision(7, 2);
    }
}
