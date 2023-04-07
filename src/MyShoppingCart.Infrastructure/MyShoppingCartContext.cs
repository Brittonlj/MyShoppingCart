using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Infrastructure;

public sealed class MyShoppingCartContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<SecurityClaim> Claims { get; set; }
  

    public MyShoppingCartContext(){ }
    public MyShoppingCartContext(DbContextOptions<MyShoppingCartContext> context) : base(context) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMyShoppingCartInfrastructureMarker).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return Database.BeginTransaction();
    }
}
