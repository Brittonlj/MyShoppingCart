namespace MyShoppingCart.Application.Tests.Customers;

public class CreateCustomerQueryValidatorTests
{
    private readonly IValidator<CreateCustomerQuery> _validator = new CreateCustomerQueryValidator();
    private const string LONG_STRING = "xznbqnpwltundqbukbmykggwceuunackjjfoccalylyugrfxqme";

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = GetCreateCustomerQuery();

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region First Name
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenFirstNameIsEmpty()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { FirstName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.FirstName));
        results.Errors[0].ErrorMessage.Should().Be("'First Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenFirstNameIsTooLong()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { FirstName = LONG_STRING };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.FirstName));
        results.Errors[0].ErrorMessage.Should().Be("The length of 'First Name' must be 50 characters or fewer. You entered 51 characters.");
    }
    #endregion

    #region Last Name
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameIsEmpty()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { LastName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.LastName));
        results.Errors[0].ErrorMessage.Should().Be("'Last Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsEmpty()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { Email = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.Email));
        results.Errors[0].ErrorMessage.Should().Be("'Email' must not be empty.");
    }
    #endregion

    #region Email
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameTooLong()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { LastName = LONG_STRING };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.LastName));
        results.Errors[0].ErrorMessage.Should().Be("The length of 'Last Name' must be 50 characters or fewer. You entered 51 characters.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailTooLong()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { Email = LONG_STRING };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.Email));
        results.Errors[0].ErrorMessage.Should().Be("The length of 'Email' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[1].PropertyName.Should().Be(nameof(CreateCustomerQuery.Email));
        results.Errors[1].ErrorMessage.Should().Be("'Email' is not a valid email address.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsInvalid()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { Email = "test" };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.Email));
        results.Errors[0].ErrorMessage.Should().Be("'Email' is not a valid email address.");
    }
    #endregion

    #region Billing Address

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsNull()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { BillingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.BillingAddress));
        results.Errors[0].ErrorMessage.Should().Be("'Billing Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsInvalid()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with
        {
            BillingAddress = new AddressModel(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be("BillingAddress.Street");
        results.Errors[0].ErrorMessage.Should().Be("'Street' must not be empty.");
        results.Errors[1].PropertyName.Should().Be("BillingAddress.City");
        results.Errors[1].ErrorMessage.Should().Be("'City' must not be empty.");
        results.Errors[2].PropertyName.Should().Be("BillingAddress.State");
        results.Errors[2].ErrorMessage.Should().Be("'State' must not be empty.");
        results.Errors[3].PropertyName.Should().Be("BillingAddress.PostalCode");
        results.Errors[3].ErrorMessage.Should().Be("'Postal Code' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsTooLong()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with
        {
            BillingAddress = new AddressModel(LONG_STRING, LONG_STRING, LONG_STRING, LONG_STRING)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be("BillingAddress.Street");
        results.Errors[0].ErrorMessage.Should().Be("The length of 'Street' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[1].PropertyName.Should().Be("BillingAddress.City");
        results.Errors[1].ErrorMessage.Should().Be("The length of 'City' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[2].PropertyName.Should().Be("BillingAddress.State");
        results.Errors[2].ErrorMessage.Should().Be("The length of 'State' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[3].PropertyName.Should().Be("BillingAddress.PostalCode");
        results.Errors[3].ErrorMessage.Should().Be("The length of 'Postal Code' must be 10 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Shipping Address

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsNull()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with { ShippingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be(nameof(CreateCustomerQuery.ShippingAddress));
        results.Errors[0].ErrorMessage.Should().Be("'Shipping Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsInvalid()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with
        {
            ShippingAddress = new AddressModel(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be("ShippingAddress.Street");
        results.Errors[0].ErrorMessage.Should().Be("'Street' must not be empty.");
        results.Errors[1].PropertyName.Should().Be("ShippingAddress.City");
        results.Errors[1].ErrorMessage.Should().Be("'City' must not be empty.");
        results.Errors[2].PropertyName.Should().Be("ShippingAddress.State");
        results.Errors[2].ErrorMessage.Should().Be("'State' must not be empty.");
        results.Errors[3].PropertyName.Should().Be("ShippingAddress.PostalCode");
        results.Errors[3].ErrorMessage.Should().Be("'Postal Code' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsTooLong()
    {
        //Arrange
        var query = GetCreateCustomerQuery() with
        {
            ShippingAddress = new AddressModel(LONG_STRING, LONG_STRING, LONG_STRING, LONG_STRING)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.NotBeEmpty();
        results.Errors[0].PropertyName.Should().Be("ShippingAddress.Street");
        results.Errors[0].ErrorMessage.Should().Be("The length of 'Street' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[1].PropertyName.Should().Be("ShippingAddress.City");
        results.Errors[1].ErrorMessage.Should().Be("The length of 'City' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[2].PropertyName.Should().Be("ShippingAddress.State");
        results.Errors[2].ErrorMessage.Should().Be("The length of 'State' must be 50 characters or fewer. You entered 51 characters.");
        results.Errors[3].PropertyName.Should().Be("ShippingAddress.PostalCode");
        results.Errors[3].ErrorMessage.Should().Be("The length of 'Postal Code' must be 10 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Private Helpers

    private static CreateCustomerQuery GetCreateCustomerQuery()
    {
        var address = new AddressModel(
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new CreateCustomerQuery(
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            address,
            address);
    }

    #endregion
}
