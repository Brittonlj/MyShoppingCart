using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder
            .ToTable("ProductCategory")
            .HasKey(x => new { x.ProductId, x.CategoryId });

        builder
            .HasData(new ProductCategory(
                new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"),
                new Guid("2478B36B-05F0-4C89-97AB-1A2B8DD0158B")),
            new ProductCategory(
                new Guid("9955F4D7-3E40-4111-A76D-23406F93334B"),
                new Guid("AFA2903E-6595-4F62-9BC2-577F0399CE18")),
            new ProductCategory(
                new Guid("AD7D0CF7-CE00-477D-AE2A-5691F65EBA0E"),
                new Guid("5202CC15-64BB-4C2B-8BD3-BB9190782A31")),
            new ProductCategory(
                new Guid("AD7D0CF7-CE00-477D-AE2A-5691F65EBA0E"),
                new Guid("31654A1C-14D6-47E5-BDF6-0C158AAD9CC9")),
            new ProductCategory(
                new Guid("E226D6B2-324F-4508-B5E5-0DB77B345C69"),
                new Guid("592CA9E1-89EE-4807-8070-7E4FE75CE92F")),
            new ProductCategory(
                new Guid("E3B2BCCE-A8F4-4F7E-9C9E-6AC93E03554A"),
                new Guid("C231F98E-F62E-4672-A12C-18C98B7E7669")),
            new ProductCategory(
                new Guid("1CAA7FB0-8C2E-4304-A1EC-747A89623131"),
                new Guid("5330FDA9-5934-4D84-936E-7E910EE66CD6")),
            new ProductCategory(
                new Guid("A9C15177-E1A4-4DC8-BCB0-5D78128FDEAE"),
                new Guid("663AC0E4-6265-417C-89E8-968B42077169")),
            new ProductCategory(
                new Guid("24EF70C3-0FC1-48D7-994F-380D4C533419"),
                new Guid("C231F98E-F62E-4672-A12C-18C98B7E7669")),
            new ProductCategory(
                new Guid("2DF1A80E-651A-417A-9028-B81D30A9A26E"),
                new Guid("663AC0E4-6265-417C-89E8-968B42077169")),
            new ProductCategory(
                new Guid("0553CA62-284D-4379-AFC5-C2D4903F7A4C"),
                new Guid("A306808A-6156-44B9-8DAB-07F05039FA33")));
    }
}
