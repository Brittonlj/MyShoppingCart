namespace MyShoppingCart.Integration.Tests.Authentication;

[Collection("Integration Tests")]
public class RegisterTests
{
    private const string ROUTE_TO_TEST = "/authentication/register";
    private readonly HttpClient _client;

    public RegisterTests(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
    }

    #region Happy Path

    [Fact]
    public async Task Register_ReturnsSuccess_WhenParametersAreCorrect()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with
        {
            UserName = "NewFred",
            Email = "new.fred.flintstone@test.com"
        };

        //Act
        var response = await _client.PostAsJsonAsync(ROUTE_TO_TEST, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var customer = await response.Content.ReadFromJsonAsync<CustomerModel>();
        Assert.NotNull(customer);
        customer.FirstName.Should().Be(request.FirstName);
        customer.LastName.Should().Be(request.LastName);
        customer.Email.Should().Be(request.Email);
        customer.UserName.Should().Be(request.UserName);
        Assert.NotNull(customer.ShippingAddress);
        Assert.NotNull(request.ShippingAddress);
        customer.ShippingAddress.Street.Should().Be(request.ShippingAddress.Street);
        customer.ShippingAddress.City.Should().Be(request.ShippingAddress.City);
        customer.ShippingAddress.State.Should().Be(request.ShippingAddress.State);
        customer.ShippingAddress.PostalCode.Should().Be(request.ShippingAddress.PostalCode);
        Assert.NotNull(customer.BillingAddress);
        Assert.NotNull(request.BillingAddress);
        customer.BillingAddress.Street.Should().Be(request.BillingAddress.Street);
        customer.BillingAddress.City.Should().Be(request.BillingAddress.City);
        customer.BillingAddress.State.Should().Be(request.BillingAddress.State);
        customer.BillingAddress.PostalCode.Should().Be(request.BillingAddress.PostalCode);
    }

    #endregion

    #region Already Exists

    [Fact]
    public async Task Register_ReturnFailure_WhenEmailAlreadyExists()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with { UserName = "new.fred.flintstone" };

        //Act
        var response = await _client.PostAsJsonAsync(ROUTE_TO_TEST, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
        var errors = JsonSerializer.Deserialize<ErrorList>(errorResponse.Detail);
        Assert.NotNull(errors);
        errors[0].Code.Should().Be("DuplicateEmail");
        errors[0].Message.Should().Be("Email 'fred.flintstone@test.com' is already taken.");
    }

    [Fact]
    public async Task Register_ReturnFailure_WhenUserNameAlreadyExists()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with
        {
            Email = "new.fred.flintstone@test.com"
        };

        //Act
        var response = await _client.PostAsJsonAsync(ROUTE_TO_TEST, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
        var errors = JsonSerializer.Deserialize<ErrorList>(errorResponse.Detail);
        Assert.NotNull(errors);
        errors[0].Code.Should().Be("DuplicateUserName");
        errors[0].Message.Should().Be("UserName 'fred.flintstone' is already taken.");
    }

    #endregion

    [Fact]
    public async Task Register_ReturnValidationFailure_WhenFirstNameIsEmpty()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with { FirstName = string.Empty };

        //Act
        var response = await _client.PostAsJsonAsync(ROUTE_TO_TEST, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
        var errors = errorResponse.Errors;
        Assert.NotNull(errors);
        errors["FirstName"][0].Should().Be("'First Name' must not be empty.");
    }

    [Fact]
    public async Task Register_ReturnValidationFailure_WhenLastNameIsEmpty()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with { LastName = string.Empty };

        //Act
        var response = await _client.PostAsJsonAsync(ROUTE_TO_TEST, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorResponse);
        var errors = errorResponse.Errors;
        Assert.NotNull(errors);
        errors["LastName"][0].Should().Be("'Last Name' must not be empty.");
    }

}
