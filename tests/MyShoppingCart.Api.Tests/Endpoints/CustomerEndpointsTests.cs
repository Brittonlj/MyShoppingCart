using MyShoppingCart.Application.Customers;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class CustomerEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors = 
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

    #region GetAllCustomers
    [Fact]
    public async Task GetAllCustomers_ShouldReturnCustomers_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new GetCustomersQuery();
        var response = Response<IReadOnlyList<CustomerModel>>.FromSuccess(DataProvider.GetCustomerModels());
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<CustomerModel>>)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            request,            
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = new GetCustomersQuery("UnavailableName", "UnavailableEmail");
        var response = Response<IReadOnlyList<CustomerModel>>.FromSuccess(DataProvider.GetEmptyCustomerModelsList());
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<CustomerModel>>)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new GetCustomersQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<IReadOnlyList<CustomerModel>>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult) await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new GetCustomersQuery(SortColumn: "InvalidSortColumn");
        const string ERROR_KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<IReadOnlyList<CustomerModel>>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetAllCustomers(
            _mockMediator.Object,
            request,
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
        var request = new GetCustomerQuery(Guid.NewGuid());
        var response = Response<CustomerModel>.FromSuccess(DataProvider.GetCustomerModel());
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (Ok<CustomerModel>)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnNotFound_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = new GetCustomerQuery(Guid.NewGuid());
        var response = Response<CustomerModel>.FromNotFound();
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new GetCustomerQuery(Guid.NewGuid());
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<CustomerModel>.FromErrorList(_errors);
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new GetCustomerQuery(Guid.NewGuid());
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<CustomerModel>.FromValidationFailure(_validationErrors);
        SetupMediator<GetCustomerQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.GetCustomerById(
            _mockMediator.Object,
            request,
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
        var customerModel = DataProvider.GetCustomerModel();
        var query = QueryProvider.GetCreateCustomerQuery();
        var response = Response<CustomerModel>.FromSuccess(customerModel);
        SetupMediator<CreateCustomerQuery>(response);

        //Act
        var httpResult = (Ok<CustomerModel>)await CustomerEndpoints.CreateCustomer(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be((CustomerModel)response);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var customer = DataProvider.GetCustomerModel();
        var query = QueryProvider.GetCreateCustomerQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<CustomerModel>.FromErrorList(_errors);
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
        var customer = DataProvider.GetCustomerModel();
        customer.FirstName = string.Empty;
        var query = QueryProvider.GetCreateCustomerQuery();
        const string ERROR_KEY = "FirstName";
        const string ERROR_MESSAGE = "FirstName cannot be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<CustomerModel>.FromValidationFailure(_validationErrors);
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
        var query = QueryProvider.GetUpdateCustomerQuery();
        var response = Response<CustomerModel>.FromSuccess(DataProvider.GetCustomerModel());
        SetupMediator<UpdateCustomerQuery>(response);

        //Act
        var httpResult = (Ok<CustomerModel>)await CustomerEndpoints.UpdateCustomer(
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
        var query = QueryProvider.GetUpdateCustomerQuery();
        var response = Response<CustomerModel>.FromNotFound();
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
        var query = QueryProvider.GetUpdateCustomerQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<CustomerModel>.FromErrorList(_errors);
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
        var query = QueryProvider.GetUpdateCustomerQuery() with { CustomerId = Guid.Empty };
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<CustomerModel>.FromValidationFailure(_validationErrors);
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
        var request = new DeleteCustomerCommand(Guid.NewGuid());
        var response = Response<Success>.FromSuccess(Success.Instance);
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnNotFound_WhenBadCustomerIdIsChosen()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.NewGuid());
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.NewGuid());
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new DeleteCustomerCommand(Guid.Empty);
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await CustomerEndpoints.DeleteCustomer(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Private Helpers
    private void SetupMediator(Response<IReadOnlyList<CustomerModel>> response)
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

    private void SetupMediator<T>(Response<CustomerModel> response)
        where T : class, IQuery<CustomerModel>
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<T>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    #endregion
}
