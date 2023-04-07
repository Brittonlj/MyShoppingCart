namespace MyShoppingCart.Domain.Entities;

public static class EntityUtility
{
    public static void MergeEntityLists<T>(DbSet<T> dbSet, List<T> original, List<T> requests) 
        where T : class, IEntity<Guid>
    {
        // Update
        var join = original.Join(requests,
            entityId => entityId.Id,
            requestId => requestId.Id,
            (entity, request) => new
            {
                Entity = entity,
                Request = request
            });
        var itemsToUpdate = join
            .Where(x => x.Entity.Id == x.Request.Id && x.Entity != x.Request).ToList();
        foreach (var item in itemsToUpdate)
        {
            dbSet.Entry(item.Entity).CurrentValues.SetValues(item.Request);
        }

        // Add
        var itemsToAdd = requests.Where(x => !original.Any(y => x.Id == y.Id)).ToList();
        dbSet.AddRange(itemsToAdd);

        // Delete
        var itemsToDelete = original.Where(x => !requests.Any(y => x.Id == y.Id)).ToList();
        dbSet.RemoveRange(itemsToDelete);
    }

}
