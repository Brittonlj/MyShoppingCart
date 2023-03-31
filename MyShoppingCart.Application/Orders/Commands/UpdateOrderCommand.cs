﻿namespace MyShoppingCart.Application.Orders.Commands;

public sealed record UpdateOrderCommand(Order Order, Guid? RequestingCustomerId = null) : 
    IRequest<Response<Success>>
{
}
