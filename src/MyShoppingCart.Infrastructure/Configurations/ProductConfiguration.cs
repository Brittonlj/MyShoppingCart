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

        builder
            .HasData(
            new Product
            {
                Id = new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"),
                Name = "Nike Tennis Shoes",
                Description = "These are some dope Nike Tennis Shoes!",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("9955F4D7-3E40-4111-A76D-23406F93334B"),
                Name = "Fruit Stripe Gum",
                Description = "This is some tasty gum, but the flavor doesn't last!",
                Price = 1.99M
            },
            new Product
            {
                Id = new Guid("AD7D0CF7-CE00-477D-AE2A-5691F65EBA0E"),
                Name = "Cheerios",
                Description = "Cheerios are a healthy part of your breakfast!",
                Price = 6.00M
            },
            new Product
            {
                Id = new Guid("E226D6B2-324F-4508-B5E5-0DB77B345C69"),
                Name = "7Up",
                Description = "Crisp and clean with no caffeine!",
                Price = 1.50M
            },
            new Product
            {
                Id = new Guid("E3B2BCCE-A8F4-4F7E-9C9E-6AC93E03554A"),
                Name = "A Plaid Flannel Shirt",
                Description = "The 90s are calling and they want you back!",
                Price = 20.00M
            },
            new Product
            {
                Id = new Guid("1CAA7FB0-8C2E-4304-A1EC-747A89623131"),
                Name = "Garbage Pale Kids Stickers",
                Description = "The 80s are calling and they want you back!",
                Price = 4.00M
            },
            new Product
            {
                Id = new Guid("A9C15177-E1A4-4DC8-BCB0-5D78128FDEAE"),
                Name = "Pink Stuffed Dinosaur",
                Description = "Raaawwwrrrr!",
                Price = 15.99M
            },
            new Product
            {
                Id = new Guid("24EF70C3-0FC1-48D7-994F-380D4C533419"),
                Name = "Black Trenchcoat",
                Description = "Dark and mysterious!  Good for 2 kids who want to impersonate an adult.",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("2DF1A80E-651A-417A-9028-B81D30A9A26E"),
                Name = "Monopoly",
                Description = "The game that destroys friendships and families!",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("0553CA62-284D-4379-AFC5-C2D4903F7A4C"),
                Name = "A Dog Collar",
                Description = "Heavy duty!  Fits most dogs and some people.",
                Price = 100.00M
            });
    }
}
