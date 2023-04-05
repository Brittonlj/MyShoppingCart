using MyShoppingCart.Application.Customers;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class CustomerEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly IOptionsSnapshot<MyShoppingCartSettings> _settings = 
        SettingsHelper.GetMyShoppingCartSettings();
    private readonly Dictionary<string, string[]> _validationErrors = 
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

    #region GetAllCustomers
    [Fact]
    public async Task GetAllCustomers_ShouldReturnCustomers_WhenValidParametersAreChosen()
    {
        //Arrange
        var response = Response<IReadOnlyList<Customer>>.FromSuccess(GetCustomerList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Customer>>)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            _settings,
            null,
            null,
            null,
            null,
            null,
            null,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var response = Response<IReadOnlyList<Customer>>.FromSuccess(GetEmptyCustomersList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Customer>>)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            _settings,
            "UnavailableName",
            "UnavailableEmail",
            null,
            null,
            null,
            null,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<IReadOnlyList<Customer>>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult) await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            _settings,
            null,
            null,
            null,
            null,
            null,
            null,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<IReadOnlyList<Customer>>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            _settings,
            null,
            null,
            null,
            null,
            "InvalidSortColumn",
            null,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }
    #endregion

    #region GetCustomerById

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomer_WhenValidParametersAreChosen()
    {
        //Arrange
        var response = Response<Customer>.FromSuccess(GetCustomer()); ;
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (Ok<Customer>)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnNotFound_WhenBadParametersAreChosen()
    {
        //Arrange
        var response = Response<Customer>.FromNotFound();
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Customer>.FromErrorList(_errors);
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Customer>.FromValidationFailure(_validationErrors);
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            Guid.Empty,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region CreateCustomer

    [Fact]
    public async Task CreateCustomer_ShouldReturnCustomer_WhenValidParametersAreChosen()
    {
        //Arrange
        var customer = GetCustomer();
        var query = GetCreateCustomerQuery();
        var response = Response<Customer>.FromSuccess(customer); ;
        SetupMediator<CreateCustomerQuery>(response);

        //Act
        var httpResult = (Ok<Customer>)await CustomerEndpoints.CreateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be((Customer)response);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var customer = GetCustomer();
        var query = GetCreateCustomerQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Customer>.FromErrorList(_errors);
        SetupMediator<CreateCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.CreateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var customer = GetCustomer();
        customer.FirstName = string.Empty;
        var query = GetCreateCustomerQuery();
        const string ERROR_KEY = "FirstName";
        const string ERROR_MESSAGE = "FirstName cannot be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Customer>.FromValidationFailure(_validationErrors);
        SetupMediator<CreateCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.CreateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }
    #endregion

    #region UpdateCustomer

    [Fact]
    public async Task UpdateCustomer_ShouldReturnCustomer_WhenValidParametersAreChosen()
    {
        //Arrange
        var query = GetUpdateCustomerQuery();
        var response = Response<Customer>.FromSuccess(GetCustomer()); ;
        SetupMediator<UpdateCustomerQuery>(response);

        //Act
        var httpResult = (Ok<Customer>)await CustomerEndpoints.UpdateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldReturnNotFound_WhenBadCustomerIdIsChosen()
    {
        //Arrange
        var query = GetUpdateCustomerQuery();
        var response = Response<Customer>.FromNotFound();
        SetupMediator<UpdateCustomerQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            CustomerEndpoints.UpdateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCustomer_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var query = GetUpdateCustomerQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Customer>.FromErrorList(_errors);
        SetupMediator<UpdateCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.UpdateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var query = GetUpdateCustomerQuery();
        query = query with { CustomerId = Guid.Empty };
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Customer>.FromValidationFailure(_validationErrors);
        SetupMediator<UpdateCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.UpdateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region DeleteCustomer

    [Fact]
    public async Task DeleteCustomer_ShouldReturnOk_WhenValidParametersAreChosen()
    {
        //Arrange
        var response = Response<Success>.FromSuccess(Success.Instance); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnNotFound_WhenBadCustomerIdIsChosen()
    {
        //Arrange
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            Guid.Empty,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Private Helpers
    private void SetupMediator(Response<IReadOnlyList<Customer>> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator(Response<Success> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator<T>(Response<Customer> response)
        where T : class, IQuery<Customer>
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<T>(), _cancellationToken))
            .ReturnsAsync(response);
    }

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

    private static UpdateCustomerQuery GetUpdateCustomerQuery()
    {
        var address = new Address
        {
            Street = "123 Test Street",
            City = "Test Town",
            State = "MO",
            PostalCode = "12345"
        };
        return new UpdateCustomerQuery(
            Guid.NewGuid(),
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            address,
            address);
    }

    private static IReadOnlyList<Customer> GetEmptyCustomersList()
    {
        return new List<Customer>();
    }

    private static IReadOnlyList<Customer> GetCustomerList()
    {
        var customers = new List<Customer>
        {
            new Customer
            {
                FirstName = "Bob",
                LastName = "Builder",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                FirstName = "George",
                LastName = "Jetson",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },

        };

        return customers;
    }

    private static Customer GetCustomer()
    {
        var customer = new Customer
        {
            FirstName = "George",
            LastName = "Jetson",
            Email = "bob.builder@test.com",
            BillingAddress = new Address
            {
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            },
            ShippingAddress = new Address
            {
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            }
        };

        return customer;

    }
    #endregion
}
