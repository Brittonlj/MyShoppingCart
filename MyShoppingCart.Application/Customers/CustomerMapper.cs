using MyShoppingCart.Application.Addresses;

namespace MyShoppingCart.Application.Customers;

public static class CustomerMapper
{
    public static Customer ToEntity(this CreateCustomerQuery other)
    {
        var billingAddress = other.BillingAddress.ToEntity();
        var shippingAddress = other.ShippingAddress.ToEntity();

        return new Customer
        {
            FirstName = other.FirstName,
            LastName = other.LastName,
            Email = other.Email,
            BillingAddress = billingAddress,
            BillingAddressId = billingAddress.Id,
            ShippingAddress = shippingAddress,
            ShippingAddressId = shippingAddress.Id
        };
    }

    public static CustomerModel ToModel(this Customer other)
    {
        var billingAddress = other.BillingAddress.ToModel();
        var shippingAddress = other.ShippingAddress.ToModel();

        return new CustomerModel(
            other.Id,
            other.FirstName,
            other.LastName,
            other.Email,
            billingAddress,
            shippingAddress);
    }
}
