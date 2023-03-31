using Microsoft.EntityFrameworkCore;

namespace MyShoppingCart.Application.Data;

public static class DataExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderBy(source, orderBy.Expression);
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderByDescending(source, orderBy.Expression);
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.ThenBy(source, orderBy.Expression);
    }

    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.ThenByDescending(source, orderBy.Expression);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static void HandleChanges<T>(this DbSet<T> dbSet, T? original, T? request) where T : EntityBase
    {
        if (original is not null && request is not null)
        {
            dbSet.Entry(original).CurrentValues.SetValues(request);
        }
        if (original is null && request is not null)
        {
            dbSet.Add(request);
        }
        if (original is not null && request is null)
        {
            dbSet.Remove(original);
        }
    }

    public static async void MergeList<T>(this DbSet<T> origin, List<T> requests, CancellationToken token) 
        where T : EntityBase
    {
        if (origin.SequenceEqual(requests))
        {
            return;
        }

        var same = await origin.Where(x => requests.Contains(x)).ToListAsync(token);
        same.ForEach(x => origin.Entry(x).CurrentValues.SetValues(requests.First(y => x.Id == y.Id)));

        var itemsToAdd = requests.Where(x => !origin.Contains(x));
        origin.AddRange(itemsToAdd);

        var itemsToDelete = await origin.Where(x => !requests.Contains(x)).ToListAsync(token);
        origin.RemoveRange(itemsToDelete); //TODO: Refactor to use ExecuteDelete
    }
}
