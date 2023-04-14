using Ardalis.GuardClauses;
using MediatR;
using MyShoppingCart.Application.Customers;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Cli.Handlers;

internal sealed class AddCustomerHandler : ICommandHandler
{
    private readonly IMediator _mediator;

    public AddCustomerHandler(IMediator mediator)
    {
        _mediator = Guard.Against.Null(mediator);
    }

    public async Task<string> Handle(CliArguments args)
    {
        Console.WriteLine();

        string? firstName = null;
        while (string.IsNullOrWhiteSpace(firstName))
        {
            Console.Write("Enter First Name: ");
            firstName = Console.ReadLine();
        }

        string? lastName = null;
        while (string.IsNullOrWhiteSpace(lastName))
        {
            Console.Write("Enter Last Name: ");
            lastName = Console.ReadLine();
        }

        string? email = null;
        while (string.IsNullOrWhiteSpace(email))
        {
            Console.Write("Enter Email: ");
            email = Console.ReadLine();
        }

        string? userName = null;
        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.Write("Enter UserName: ");
            userName = Console.ReadLine();
        }

        string? password = null;
        while (string.IsNullOrWhiteSpace(password))
        {
            Console.Write("Enter Password: ");
            password = Console.ReadLine();
        }

        Console.WriteLine();
        Console.WriteLine("Shipping Address");

        string? shipping_street = null;
        while (string.IsNullOrWhiteSpace(shipping_street))
        {
            Console.Write("Enter Street Address: ");
            shipping_street = Console.ReadLine();
        }

        string? shipping_city = null;
        while (string.IsNullOrWhiteSpace(shipping_city))
        {
            Console.Write("Enter City: ");
            shipping_city = Console.ReadLine();
        }

        string? shipping_state = null;
        while (string.IsNullOrWhiteSpace(shipping_state))
        {
            Console.Write("Enter State: ");
            shipping_state = Console.ReadLine();
        }

        string? shipping_postal = null;
        while (string.IsNullOrWhiteSpace(shipping_postal))
        {
            Console.Write("Enter Postal Code: ");
            shipping_postal = Console.ReadLine();
        }

        Console.WriteLine();
        Console.WriteLine("Billing Address");

        string? billing_street = null;
        while (string.IsNullOrWhiteSpace(billing_street))
        {
            Console.Write("Enter Street Address: ");
            billing_street = Console.ReadLine();
        }

        string? billing_city = null;
        while (string.IsNullOrWhiteSpace(billing_city))
        {
            Console.Write("Enter City: ");
            billing_city = Console.ReadLine();
        }

        string? billing_state = null;
        while (string.IsNullOrWhiteSpace(billing_state))
        {
            Console.Write("Enter State: ");
            billing_state = Console.ReadLine();
        }

        string? billing_postal = null;
        while (string.IsNullOrWhiteSpace(billing_postal))
        {
            Console.Write("Enter Postal Code: ");
            billing_postal = Console.ReadLine();
        }

        var command = new CreateCustomerQuery(
            firstName,
            lastName,
            email,
            userName,
            password,
            new AddressModel(
                billing_street,
                billing_city,
                billing_state,
                billing_postal),
            new AddressModel(
                shipping_street,
                shipping_city,
                shipping_state,
                shipping_postal));

        var response = await _mediator.Send(command);

        return response.Match(
            success => $"Customer {success.Id} was created.",
            unauthorized => "Access Forbidden.",
            notFound => $"Invalid use case <NotFound>.",
            error => error.ToString(),
            validationFailed => validationFailed.ToString());
    }
}
