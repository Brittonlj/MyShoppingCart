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

        builder
            .HasData(
            new Customer
            {
                Id = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                BillingAddressId = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
                ShippingAddressId = new Guid("6B760260-799C-4AF1-A173-0BF83A2A74D5"),
                ConcurrencyStamp = "998eaff7-78dd-4efc-a16e-5a23a4053398",
                EmailConfirmed = true,
                NormalizedEmail = "FRED.FLINTSTONE@TEST.COM",
                NormalizedUserName = "FRED.FLINTSTONE",
                PasswordHash = "AQAAAAIAAYagAAAAEKy9NjMnPf12p4csSvLOiAmdC5zHZnhaF1DkgGH7+e9im6aIuYftYn/cqQP5qgDgLA==",
                SecurityStamp = "DVI25ATQSEVFM2MVEPL45HBEWPLT6DNG",
                UserName = "fred.flintstone"
            },
            new Customer
            {
                Id = new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                FirstName = "George",
                LastName = "Jetson",
                Email = "george.jetson@test.com",
                BillingAddressId = new Guid("B592FA04-541A-4BF2-967C-C07468AF2014"),
                ShippingAddressId = new Guid("CCB9F54B-F5A0-4D42-927D-C65294E0F629"),
                ConcurrencyStamp = "02fe5045-5c7b-4641-90a0-e9c5d375fb7b",
                EmailConfirmed = true,
                NormalizedEmail = "GEORGE.JETSON@TEST.COM",
                NormalizedUserName = "GEORGE.JETSON",
                PasswordHash = "AQAAAAIAAYagAAAAEDvfuUmbZTWsI9Xgb//t60GssHdXbjTzIh7MIxZ6FGCRjcWIQs14ZCMXjkuYKetxKA==",
                SecurityStamp = "T5MUNAWWSMQHJTKUYXXI35K2OQ6O4Q7D",
                UserName = "george.jetson"
            }

            );
            //""
    }
}
