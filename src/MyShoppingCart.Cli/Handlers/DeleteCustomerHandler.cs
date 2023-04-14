using MediatR;
using MyShoppingCart.Application.Customers;
using Ardalis.GuardClauses;

namespace MyShoppingCart.Cli.Handlers;

internal sealed class DeleteCustomerHandler : ICommandHandler
{
    private readonly IMediator _mediator;

    public DeleteCustomerHandler(IMediator mediator)
    {
        _mediator = Guard.Against.Null(mediator);
    }

    public async Task<string> Handle(CliArguments args)
    {
        if (string.IsNullOrWhiteSpace(args.CustomerId) || !Guid.TryParse(args.CustomerId, out var customerId))
        {
            return "Invalid Customer ID.";
        }

        var command = new DeleteCustomerCommand(customerId);

        var response = await _mediator.Send(command);

        return response.Match(
            success => $"Customer {customerId} was deleted.",
            unauthorized => "Access Forbidden.",
            notFound => $"Customer {customerId} was not found.",
            error => error.ToString(),
            validationFailed => validationFailed.ToString());
    }
}
