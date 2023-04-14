using Ardalis.GuardClauses;
using MediatR;
using MyShoppingCart.Application.Authentication;

namespace MyShoppingCart.Cli.Handlers;

internal class SetPasswordHandler : ICommandHandler
{
    private readonly IMediator _mediator;

    public SetPasswordHandler(IMediator mediator)
    {
        _mediator = Guard.Against.Null(mediator);
    }

    public async Task<string> Handle(CliArguments args)
    {
        if (string.IsNullOrWhiteSpace(args.CustomerId) || !Guid.TryParse(args.CustomerId, out var customerId))
        {
            return "Invalid Customer ID.";
        }

        Console.WriteLine();
        string? firstPassword = null;
        while (string.IsNullOrWhiteSpace(firstPassword))
        {
            Console.Write("Enter Password: ");
            firstPassword = Console.ReadLine();
        }

        string? secondPassword = null;
        while (string.IsNullOrWhiteSpace(secondPassword))
        {
            Console.Write("Enter Password: ");
            secondPassword = Console.ReadLine();
        }

        if (firstPassword != secondPassword)
        {
            return "Passwords do not match.";
        }

        var request = new SetPasswordCommand(customerId, firstPassword);

        var response = await _mediator.Send(request);

        return response.Match(
            success => "Password set.",
            unauthorized => "Access Forbidden.",
            notFound => $"Customer {args.CustomerId} was not found.",
            error => error.ToString(),
            validationFailed => validationFailed.ToString());
    }
}