using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder
            .ToTable("LineItem");

        builder
            .HasKey(x => new { x.OrderId, x.ProductId })
            .IsClustered();

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .HasPrincipalKey(x => x.Id);

        builder
            .HasIndex(x => x.OrderId);
    }
}
