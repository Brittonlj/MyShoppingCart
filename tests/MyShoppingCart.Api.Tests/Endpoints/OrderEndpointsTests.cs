using MyShoppingCart.Application.Orders;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class OrderEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

    #region GetAllOrders
    [Fact]
    public async Task GetAllOrders_ShouldReturnOrders_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        var response = Response<IReadOnlyList<Order>>.FromSuccess(DataProvider.GetOrders()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Order>>)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnNoOrders_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        var response = Response<IReadOnlyList<Order>>.FromSuccess(DataProvider.GetEmptyOrdersList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Order>>)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<IReadOnlyList<Order>>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = QueryProvider.GetGetOrdersQuery();
        const string ERROR_KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<IReadOnlyList<Order>>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }
    #endregion

    #region GetOrderById

    [Fact]
    public async Task GetOrderById_ShouldReturnOrder_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrderQuery();
        var response = Response<Order>.FromSuccess(DataProvider.GetOrder()); ;
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (Ok<Order>)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnNotFound_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrderQuery();
        var response = Response<Order>.FromNotFound();
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = QueryProvider.GetGetOrderQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Order>.FromErrorList(_errors);
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = QueryProvider.GetGetOrderQuery() with { OrderId = Guid.Empty };
        const string ERROR_KEY = "OrderId";
        const string ERROR_MESSAGE = "'Order Id' may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Order>.FromValidationFailure(_validationErrors);
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region CreateOrder

    [Fact]
    public async Task CreateOrder_ShouldReturnOrder_WhenValidParametersAreChosen()
    {
        //Arrange
        var order = DataProvider.GetOrder();
        var query = QueryProvider.GetCreateOrderQuery();
        var response = Response<Order>.FromSuccess(order); ;
        SetupMediator<CreateOrderQuery>(response);

        //Act
        var httpResult = (Ok<Order>)await OrderEndpoints.CreateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be((Order)response);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var order = DataProvider.GetOrder();
        var query = QueryProvider.GetCreateOrderQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Order>.FromErrorList(_errors);
        SetupMediator<CreateOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.CreateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var query = QueryProvider.GetCreateOrderQuery() with { CustomerId = Guid.Empty };
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "'Customer Id' cannot be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Order>.FromValidationFailure(_validationErrors);
        SetupMediator<CreateOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.CreateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }
    #endregion

    #region UpdateOrder

    [Fact]
    public async Task UpdateOrder_ShouldReturnOrder_WhenValidParametersAreChosen()
    {
        //Arrange
        var query = QueryProvider.GetUpdateOrderQuery();
        var response = Response<Order>.FromSuccess(DataProvider.GetOrder()); ;
        SetupMediator<UpdateOrderQuery>(response);

        //Act
        var httpResult = (Ok<Order>)await OrderEndpoints.UpdateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task UpdateOrder_ShouldReturnNotFound_WhenBadOrderIdIsChosen()
    {
        //Arrange
        var query = QueryProvider.GetUpdateOrderQuery();
        var response = Response<Order>.FromNotFound();
        SetupMediator<UpdateOrderQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            OrderEndpoints.UpdateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateOrder_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var query = QueryProvider.GetUpdateOrderQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Order>.FromErrorList(_errors);
        SetupMediator<UpdateOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.UpdateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task UpdateOrder_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var query = QueryProvider.GetUpdateOrderQuery() with { OrderId = Guid.Empty };
        const string ERROR_KEY = "OrderId";
        const string ERROR_MESSAGE = "OrderId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Order>.FromValidationFailure(_validationErrors);
        SetupMediator<UpdateOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.UpdateOrder(
            _mockMediator.Object,
            query,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region DeleteOrder

    [Fact]
    public async Task DeleteOrder_ShouldReturnOk_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        var response = Response<Success>.FromSuccess(Success.Instance); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnNotFound_WhenBadOrderIdIsChosen()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new DeleteOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        const string ERROR_KEY = "OrderId";
        const string ERROR_MESSAGE = "OrderId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Private Helpers
    private void SetupMediator(Response<IReadOnlyList<Order>> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetOrdersQuery>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator(Response<Success> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator<T>(Response<Order> response)
        where T : class, IQuery<Order>
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<T>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    #endregion
}
