using System.Text.Json.Serialization;

namespace MyShoppingCart.Domain.Models;

public sealed class CustomerModel : IEntity<Guid>
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    [JsonIgnore]
    public Guid? ShippingAddressId { get; set; }
    public Address? ShippingAddress { get; set; }
    [JsonIgnore]
    public Guid? BillingAddressId { get; set; }
    public Address? BillingAddress { get; set; }

}
