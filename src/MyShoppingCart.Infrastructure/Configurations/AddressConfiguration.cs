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

        builder
            .HasData(
                new Address
                {
                    Id = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
                    Street = "123 Test St",
                    City = "Bedrock",
                    State = "MO",
                    PostalCode = "12345"
                },
                new Address
                {
                    Id = new Guid("6B760260-799C-4AF1-A173-0BF83A2A74D5"),
                    Street = "123 Test St",
                    City = "Bedrock",
                    State = "MO",
                    PostalCode = "12345"
                },
                new Address
                {
                    Id = new Guid("B592FA04-541A-4BF2-967C-C07468AF2014"),
                    Street = "123 Test St",
                    City = "Space City",
                    State = "MO",
                    PostalCode = "12345"
                },
                new Address
                {
                    Id = new Guid("CCB9F54B-F5A0-4D42-927D-C65294E0F629"),
                    Street = "123 Test St",
                    City = "Space City",
                    State = "MO",
                    PostalCode = "12345"
                }
            );
    }
}
