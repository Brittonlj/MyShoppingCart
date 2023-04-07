namespace MyShoppingCart.Domain.Data;

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


    public static IOrderedQueryable<T> OrderBy<T>(this ISpecificationBuilder<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderBy(source, orderBy.Expression);
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this ISpecificationBuilder<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderByDescending(source, orderBy.Expression);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> query, int pageNumber, int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}
