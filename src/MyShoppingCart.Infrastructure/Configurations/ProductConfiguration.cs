using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private const string DescriptionText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.";

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
            .HasMany(x => x.Categories)
            .WithMany()
            .UsingEntity<ProductCategory>();

        builder
            .HasData(
            new Product
            {
                Id = new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"),
                Name = "Nike Tennis Shoes",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("9955F4D7-3E40-4111-A76D-23406F93334B"),
                Name = "Fruit Stripe Gum",
                Description = DescriptionText,
                Price = 1.99M
            },
            new Product
            {
                Id = new Guid("AD7D0CF7-CE00-477D-AE2A-5691F65EBA0E"),
                Name = "Cheerios",
                Description = DescriptionText,
                Price = 6.00M
            },
            new Product
            {
                Id = new Guid("E226D6B2-324F-4508-B5E5-0DB77B345C69"),
                Name = "7Up 16oz Bottle",
                Description = DescriptionText,
                Price = 1.50M
            },
            new Product
            {
                Id = new Guid("E3B2BCCE-A8F4-4F7E-9C9E-6AC93E03554A"),
                Name = "A Plaid Flannel Shirt",
                Description = DescriptionText,
                Price = 20.00M
            },
            new Product
            {
                Id = new Guid("1CAA7FB0-8C2E-4304-A1EC-747A89623131"),
                Name = "Garbage Pale Kids Stickers",
                Description = DescriptionText,
                Price = 4.00M
            },
            new Product
            {
                Id = new Guid("A9C15177-E1A4-4DC8-BCB0-5D78128FDEAE"),
                Name = "Pink Stuffed Dinosaur",
                Description = DescriptionText,
                Price = 15.99M
            },
            new Product
            {
                Id = new Guid("24EF70C3-0FC1-48D7-994F-380D4C533419"),
                Name = "Black Trenchcoat",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("2DF1A80E-651A-417A-9028-B81D30A9A26E"),
                Name = "Monopoly",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("0553CA62-284D-4379-AFC5-C2D4903F7A4C"),
                Name = "A Dog Collar",
                Description = DescriptionText,
                Price = 100.00M,
                ImageUrl = "/img/dog_collar.jpg"
            },
            new Product
            {
                Id = new Guid("1539ce54-3d94-4a0f-9580-da51123797cb"),
                Name = "Hubba Bubba Bubble Gum",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("5b858f9e-1f64-47b0-841f-14604a9d4035"),
                Name = "Spearmint Gum",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("092fa47a-da69-4514-985e-7eed2739d817"),
                Name = "Big League Chew Bubble Gum",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("430f8382-69c8-473f-a74f-a244a77133ae"),
                Name = "Adidas Tennis Shoes",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("77122595-d2ef-4b4b-8841-c6a5fb91acfe"),
                Name = "Tennis Racket",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("f7246fc8-8a97-4b0b-8fd6-21bb0287b6eb"),
                Name = "Golf Clubs",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("e53ce73a-3e50-4498-9c51-ef0df346a3c2"),
                Name = "Fruit Loops",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("11126500-2f81-4b90-88cb-84d8378e6d6b"),
                Name = "Mountain Dew 16oz Bottle",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("24181791-37f3-4af6-95e3-4a17409cc1be"),
                Name = "Sweet Tea 16oz Bottle",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("64068786-c23f-4904-bc30-d297fe71eb6f"),
                Name = "A Pair of Blue Jeans",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("3ab12432-5d93-496e-83c9-5929514f82b5"),
                Name = "A 4-Pack of T-Shirts",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("678c8fc3-8156-45f0-8a0a-04d747442cf9"),
                Name = "Long Underwear",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("58a0114b-e802-40d4-9b0b-7187b77eb30c"),
                Name = "A Pack of Pokemon Cards",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("9fc97e4a-5b6c-4cbd-a4ce-35bfdb49bb05"),
                Name = "A Pack of Baseball Cards",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("13ae159d-4f77-4b31-9a99-1df06086a26d"),
                Name = "G.I. Joe Action Figure",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("31e90811-9d42-4459-903c-5092d21f992f"),
                Name = "A Green Hoodie",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("32aa1795-2bbf-4a94-bacf-334fbf72a49b"),
                Name = "A Baseball Cap",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("424f2a29-f09d-43e9-a28e-7fd1750f4102"),
                Name = "A Baseball Glove",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("d71023e3-93b6-45a6-a5c0-1fdda2d4a1cf"),
                Name = "A Hockey Stick",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("6ca7d4d7-2d22-4481-b39d-890c515a20fe"),
                Name = "Cowboy Boots",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("b59eaa64-45f5-4b90-83fa-16235af1e5e3"),
                Name = "Sweat Pants",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("99ebc170-e52d-4e52-b4ac-1abbbfb799a5"),
                Name = "A Clock Radio",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("32fce956-7878-4eba-bc57-c850d58add33"),
                Name = "A Boombox",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("35afde2a-7889-43e8-925c-09b65f05c849"),
                Name = "64gb iPod",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("88f90070-9bfb-4722-a802-a58e4459f625"),
                Name = "32gb iPod",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("a6ebec19-67b5-4286-a634-a5994f332f4c"),
                Name = "Sony 42-Inch Television",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("a145dfbd-eefe-411c-8d6d-3feaf0747231"),
                Name = "Visio 46-Inch Television",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("c042cee5-b114-4fa3-8ba6-64b180ff128e"),
                Name = "A 5-Foot Leash",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("f85c2129-dc48-494e-a551-6fa684e77142"),
                Name = "A 20lb Bag of Cat Food",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("9be0417a-8c93-4cde-9d6c-79a15996b5a3"),
                Name = "A 20lb Bag of Kitty Litter",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("6d761b94-26b6-4b15-a585-8c7dd15f8e4d"),
                Name = "A 20lb Bag of Dog Food",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("916d84a6-727a-4aa5-b742-562815b28297"),
                Name = "12 Pack of HotDog Buns",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("397387de-29aa-47cd-83b1-2e1e242bbfed"),
                Name = "10 Pack of HotDogs",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("bc4766e4-fbb4-4384-b3b8-bb5c3d7cb0b0"),
                Name = "1lb of Bacon",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("a922340c-81bc-4189-9060-56b796ff954e"),
                Name = "A Dozen Donuts",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("54e911e5-a9db-423a-b284-cadcd559b30b"),
                Name = "Lite Brite",
                Description = DescriptionText,
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("e6e7723f-0b6c-4c37-b259-18de64d486cb"),
                Name = "Barrel of Monkeys",
                Description = DescriptionText,
                Price = 9.00M
            },
            new Product
            {
                Id = new Guid("8399bba9-b3eb-4e2b-9261-b6f38163f114"),
                Name = "Rubik's Cube",
                Description = DescriptionText,
                Price = 10.00M
            },
            new Product
            {
                Id = new Guid("10735d96-d843-4835-9312-4d19aa92bf5f"),
                Name = "Hungry Hungry Hippos",
                Description = DescriptionText,
                Price = 16.00M
            },
            new Product
            {
                Id = new Guid("24f97fd4-f17a-4bf3-998c-99c2ba557101"),
                Name = "Connect4",
                Description = DescriptionText,
                Price = 18.00M
            },
            new Product
            {
                Id = new Guid("41ab5b04-f9f1-4e49-8004-246fe79f6136"),
                Name = "Dungeons and Dragons Rulebook",
                Description = DescriptionText,
                Price = 50.00M
            },
            new Product
            {
                Id = new Guid("70ab156e-d382-4d9f-b48a-8efa1e74c2b7"),
                Name = "A Gallon of Sweet Tea",
                Description = DescriptionText,
                Price = 8.00M
            },
            new Product
            {
                Id = new Guid("b10f29d8-addc-4fd2-81bb-b3084db31018"),
                Name = "12 Pack of Beer",
                Description = DescriptionText,
                Price = 15.00M
            });
    }
}
