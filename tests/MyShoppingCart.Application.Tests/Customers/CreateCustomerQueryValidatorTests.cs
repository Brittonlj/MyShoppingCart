namespace MyShoppingCart.Application.Tests.Customers;

public class CreateCustomerQueryValidatorTests
{
    private readonly IValidator<CreateCustomerQuery> _validator = new CreateCustomerQueryValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery();

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

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
        var query = DataHelper.GetCreateCustomerQuery() with { FirstName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.FirstName),
            "'First Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenFirstNameIsTooLong()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { FirstName = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.FirstName),
            "The length of 'First Name' must be 50 characters or fewer. You entered 51 characters.");
    }
    #endregion

    #region Last Name
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameIsEmpty()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { LastName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.LastName),
            "'Last Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameTooLong()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { LastName = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.LastName),
            "The length of 'Last Name' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Email

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsEmpty()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { Email = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.Email),
            "'Email' must not be empty.");
    }


    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailTooLong()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { Email = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.Email),
            "The length of 'Email' must be 50 characters or fewer. You entered 51 characters.");
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.Email),
            "'Email' is not a valid email address.", 1);
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsInvalid()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { Email = "test" };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.Email),
            "'Email' is not a valid email address.");
    }
    #endregion

    #region Billing Address

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsNull()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { BillingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.BillingAddress),
            "'Billing Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsInvalid()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with
        {
            BillingAddress = new AddressModel(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            "BillingAddress.Street",
            "'Street' must not be empty.");
        results.AssertValidationErrors(
            "BillingAddress.City",
            "'City' must not be empty.", 1);
        results.AssertValidationErrors(
            "BillingAddress.State",
            "'State' must not be empty.", 2);
        results.AssertValidationErrors(
            "BillingAddress.PostalCode",
            "'Postal Code' must not be empty.", 3);
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsTooLong()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with
        {
            BillingAddress = new AddressModel(LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51)
        };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            "BillingAddress.Street",
            "The length of 'Street' must be 50 characters or fewer. You entered 51 characters.");
        results.AssertValidationErrors(
            "BillingAddress.City",
            "The length of 'City' must be 50 characters or fewer. You entered 51 characters.", 1);
        results.AssertValidationErrors(
            "BillingAddress.State",
            "The length of 'State' must be 50 characters or fewer. You entered 51 characters.", 2);
        results.AssertValidationErrors(
            "BillingAddress.PostalCode",
            "The length of 'Postal Code' must be 10 characters or fewer. You entered 51 characters.", 3);
    }

    #endregion

    #region Shipping Address

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsNull()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with { ShippingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(CreateCustomerQuery.ShippingAddress),
            "'Shipping Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsInvalid()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with
        {
            ShippingAddress = new AddressModel(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            "ShippingAddress.Street",
            "'Street' must not be empty.");
        results.AssertValidationErrors(
            "ShippingAddress.City",
            "'City' must not be empty.", 1);
        results.AssertValidationErrors(
            "ShippingAddress.State",
            "'State' must not be empty.", 2);
        results.AssertValidationErrors(
            "ShippingAddress.PostalCode",
            "'Postal Code' must not be empty.", 3);
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsTooLong()
    {
        //Arrange
        var query = DataHelper.GetCreateCustomerQuery() with
        {
            ShippingAddress = new AddressModel(LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51)
        };

        //Act
        var results = await _validator.ValidateAsync(query, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            "ShippingAddress.Street",
            "The length of 'Street' must be 50 characters or fewer. You entered 51 characters.");
        results.AssertValidationErrors(
            "ShippingAddress.City",
            "The length of 'City' must be 50 characters or fewer. You entered 51 characters.", 1);
        results.AssertValidationErrors(
            "ShippingAddress.State",
            "The length of 'State' must be 50 characters or fewer. You entered 51 characters.", 2);
        results.AssertValidationErrors(
            "ShippingAddress.PostalCode",
            "The length of 'Postal Code' must be 10 characters or fewer. You entered 51 characters.", 3);
    }

    #endregion
}
