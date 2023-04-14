namespace MyShoppingCart.Domain.Specifications;

public sealed class GetProductsSpec : BaseSpecification<Product>
{
    public const string DEFAULT_SORT_COLUMN = "Name";

    public enum SortColumns
    {
        Name,
        Description,
        Price,
    }


    public GetProductsSpec(
        string? searchString,
        int pageNumber,
        int pageSize,
        string sortColumn,
        bool sortAscending = true
    )
    {
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            Query
                .Where(x => 
                x.Name.Contains(searchString) || 
                x.Description.Contains(searchString) ||
                x.Categories.Any(x => x.Name.Contains(searchString)));
        }

        SetSorting(sortAscending, sortColumn);

        Query
            .Paginate(pageNumber, pageSize)
            .Include(x => x.Categories)
            .AsSplitQuery();
    }

    private void SetSorting(bool sortAscending, string sortColumn)
    {
        if (!Enum.TryParse<SortColumns>(sortColumn, true, out var orderByEnum))
        {
            orderByEnum = Enum.Parse<SortColumns>(DEFAULT_SORT_COLUMN);
        }

        if (sortAscending)
        {
            switch (orderByEnum)
            {
                case SortColumns.Name:
                    Query.OrderBy(x => x.Name);
                    break;
                case SortColumns.Description:
                    Query.OrderBy(x => x.Description);
                    break;
                case SortColumns.Price:
                    Query.OrderBy(x => x.Price);
                    break;
            }
        }
        else
        {
            switch (orderByEnum)
            {
                case SortColumns.Name:
                    Query.OrderByDescending(x => x.Name);
                    break;
                case SortColumns.Description:
                    Query.OrderByDescending(x => x.Description);
                    break;
                case SortColumns.Price:
                    Query.OrderByDescending(x => x.Price);
                    break;
            }
        }
    }
}
