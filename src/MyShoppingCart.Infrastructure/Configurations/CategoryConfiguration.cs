using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("Category")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasData(
                new Category(new Guid("2478B36B-05F0-4C89-97AB-1A2B8DD0158B"), "Athletic"),
                new Category(new Guid("AFA2903E-6595-4F62-9BC2-577F0399CE18"), "Gum"),
                new Category(new Guid("5202CC15-64BB-4C2B-8BD3-BB9190782A31"), "Cereal"),
                new Category(new Guid("31654A1C-14D6-47E5-BDF6-0C158AAD9CC9"), "Food"),
                new Category(new Guid("592CA9E1-89EE-4807-8070-7E4FE75CE92F"), "Beverage"),
                new Category(new Guid("C231F98E-F62E-4672-A12C-18C98B7E7669"), "Clothing"),
                new Category(new Guid("5330FDA9-5934-4D84-936E-7E910EE66CD6"), "Collectable"),
                new Category(new Guid("663AC0E4-6265-417C-89E8-968B42077169"), "Toy/Game"),
                new Category(new Guid("A306808A-6156-44B9-8DAB-07F05039FA33"), "Pet"),
                new Category(new Guid("A303508A-6156-44B9-8DAB-07F05039FA33"), "Electronics")
                );
    }
}
