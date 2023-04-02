namespace MyShoppingCart.Domain.Entities;

public static class EntityUtility
{
    public static void MergeEntityLists<T>(DbSet<T> dbSet, List<T> original, List<T> requests) 
        where T : class, IEntity<T>
    {
        var itemsToAdd = requests.Where(x => !original.Any(y => x.Id == y.Id)).ToList();
        var itemsToDelete = original.Where(x => !requests.Any(y => x.Id == y.Id)).ToList();

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

        dbSet.AddRange(itemsToAdd);

        dbSet.RemoveRange(itemsToDelete);

        foreach(var item in itemsToUpdate)
        {
            dbSet.Entry(item.Entity).CurrentValues.SetValues(item.Request);
        }
    }

}
