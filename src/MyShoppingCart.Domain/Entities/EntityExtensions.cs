namespace MyShoppingCart.Domain.Entities;

public static class EntityExtensions
{
    public static void SyncToList<TEntity>(
        this List<TEntity> originals, 
        List<TEntity> changes)
        where TEntity : class, IEntity<Guid>
    {
        var matches = originals.Join(changes,
            originalId => originalId.Id,
            changeId => changeId.Id,
            (original, change) => new { Original = original, Change = change });

        matches
            .Where(x => x.Original != x.Change)
            .ToList()
            .ForEach(x =>
            {
                x.Change.Adapt(x.Original);
            });

        //Add
        var itemsToAdd = changes.Where(x => !originals.Any(y => x.Id == y.Id));
        originals.AddRange(itemsToAdd);

        //Delete
        var itemsToDelete = originals.Where(x => !changes.Any(y => x.Id == y.Id)).ToList();

        itemsToDelete.ForEach(x => originals.Remove(x));

    }
}
