﻿namespace MyShoppingCart.Application.Orders;

public sealed class GetOrdersQueryHandler :
    IRequestHandler<GetOrdersQuery, Response<IReadOnlyList<Order>>>
{
    private readonly IRepository<Order> _orderRepository;

    public GetOrdersQueryHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = Guard.Against.Null(orderRepository);
    }

    public async Task<Response<IReadOnlyList<Order>>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new GetOrdersSpec(
            request.CustomerId,
            request.PageNumber,
            request.PageSize,
            request.SortAscending);
;
        var orders = await _orderRepository.ListAsync(spec, cancellationToken);

        return orders;
    }
}
