using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure;

public sealed class MyShoppingCartContext : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<LineItem> LineItems { get; set; }  

    public MyShoppingCartContext(){ }
    public MyShoppingCartContext(DbContextOptions<MyShoppingCartContext> context) : base(context) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMyShoppingCartInfrastructureMarker).Assembly);

        modelBuilder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Role");
        });
        modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRole");
        });
        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaim");
        });
        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogin");
        });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaim");
        });
        modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserToken");
        });
    }
}
