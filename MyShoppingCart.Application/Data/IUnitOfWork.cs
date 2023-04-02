using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyShoppingCart.Application.Data;

public interface IUnitOfWork
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<OrderProduct> OrderProducts { get; set; }

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IDbContextTransaction BeginTransaction();
}
