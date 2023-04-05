﻿namespace MyShoppingCart.Application.Tests.Validators.Customers;

public class UpdateCustomerQueryValidatorTests
{
    private readonly IValidator<UpdateCustomerQuery> _validator = new UpdateCustomerQueryValidator();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenStateIsValid()
    {
        //Arrange
        var query = GetUpdateCustomerQuery();

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { CustomerId = Guid.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.CustomerId),
            "'Customer Id' must not be empty.");
    }
    #endregion

    #region First Name
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenFirstNameIsEmpty()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { FirstName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.FirstName),
            "'First Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenFirstNameIsTooLong()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { FirstName = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.FirstName),
            "The length of 'First Name' must be 50 characters or fewer. You entered 51 characters.");
    }
    #endregion

    #region Last Name
    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameIsEmpty()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { LastName = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.LastName),
            "'Last Name' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenLastNameTooLong()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { LastName = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.LastName),
            "The length of 'Last Name' must be 50 characters or fewer. You entered 51 characters.");
    }

    #endregion

    #region Email

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsEmpty()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { Email = string.Empty };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.Email),
            "'Email' must not be empty.");
    }


    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailTooLong()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { Email = LongStrings.LONG_STRING_51 };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.Email),
            "The length of 'Email' must be 50 characters or fewer. You entered 51 characters.");
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.Email),
            "'Email' is not a valid email address.", 1);
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenEmailIsInvalid()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { Email = "test" };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.Email),
            "'Email' is not a valid email address.");
    }
    #endregion

    #region Billing Address

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsNull()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with { BillingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.BillingAddress),
            "'Billing Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenBillingAddressIsInvalid()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with
        {
            BillingAddress = new Address(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

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
        var query = GetUpdateCustomerQuery() with
        {
            BillingAddress = new Address(LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

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
        var query = GetUpdateCustomerQuery() with { ShippingAddress = null };

        //Act
        var results = await _validator.ValidateAsync(query);

        //Assert
        results.AssertValidationErrors(
            nameof(UpdateCustomerQuery.ShippingAddress),
            "'Shipping Address' must not be empty.");
    }

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenShippingAddressIsInvalid()
    {
        //Arrange
        var query = GetUpdateCustomerQuery() with
        {
            ShippingAddress = new Address(string.Empty, string.Empty, string.Empty, string.Empty)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

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
        var query = GetUpdateCustomerQuery() with
        {
            ShippingAddress = new Address(LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51, LongStrings.LONG_STRING_51)
        };

        //Act
        var results = await _validator.ValidateAsync(query);

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

    #region Private Helpers

    private static UpdateCustomerQuery GetUpdateCustomerQuery()
    {
        var address = new Address(
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new UpdateCustomerQuery(
            Guid.NewGuid(),
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            address,
            address);
    }

    #endregion
}
