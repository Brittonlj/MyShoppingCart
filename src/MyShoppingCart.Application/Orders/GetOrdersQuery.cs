using MyShoppingCart.Domain.Data;

namespace MyShoppingCart.Application.Orders;

public sealed record GetOrdersQuery(
    Guid CustomerId,
    int PageNumber = Constants.DEFAULT_PAGE_NUMBER,
    int PageSize = Constants.DEFAULT_PAGE_SIZE,
    string SortColumn = GetOrdersSpec.DEFAULT_SORT_COLUMN,
    bool SortAscending = true
    ) :
    IQueryManyPaged<Order>,
    IAuthorizedCustomerRequest
{
}
