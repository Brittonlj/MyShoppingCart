using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Data;

public interface IUnitOfWork
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<LineItem> LineItems { get; set; }
    DbSet<SecurityClaim> Claims { get; set; }


    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IDbContextTransaction BeginTransaction();
}
