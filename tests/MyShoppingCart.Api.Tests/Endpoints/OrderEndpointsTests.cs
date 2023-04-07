using MyShoppingCart.Application.Orders;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class OrderEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly IOptionsSnapshot<MyShoppingCartSettings> _settings =
        SettingsHelper.GetMyShoppingCartSettings();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

    #region GetAllOrders
    [Fact]
    public async Task GetAllOrders_ShouldReturnOrders_WhenValidParametersAreChosen()
    {
        //Arrange
        var customerId = Guid.NewGuid();
        var response = Response<IReadOnlyList<Order>>.FromSuccess(GetOrderList(customerId)); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Order>>)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            _settings,
            customerId,
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
    public async Task GetAllOrders_ShouldReturnNoOrders_WhenBadParametersAreChosen()
    {
        //Arrange
        var response = Response<IReadOnlyList<Order>>.FromSuccess(GetEmptyOrdersList()); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok<IReadOnlyList<Order>>)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            _settings,
            Guid.NewGuid(),
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
    public async Task GetAllOrders_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<IReadOnlyList<Order>>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            _settings,
            Guid.NewGuid(),
            null,
            null,
            null,
            null,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<IReadOnlyList<Order>>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetAllOrders(
            _mockMediator.Object,
            _settings,
            Guid.NewGuid(),
            null,
            null,
            "InvalidSortColumn",
            null,
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
        var response = Response<Order>.FromSuccess(GetOrder()); ;
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (Ok<Order>)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.Be(response.Success);
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnNotFound_WhenBadParametersAreChosen()
    {
        //Arrange
        var response = Response<Order>.FromNotFound();
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
           _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Order>.FromErrorList(_errors);
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "OrderId";
        const string ERROR_MESSAGE = "OrderId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Order>.FromValidationFailure(_validationErrors);
        SetupMediator<GetOrderQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.GetOrderById(
            _mockMediator.Object,
            Guid.Empty,
            Guid.NewGuid(),
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
        var order = GetOrder();
        var query = GetCreateOrderQuery();
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
        var order = GetOrder();
        var query = GetCreateOrderQuery();
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
        var order = GetOrder();
        order.CustomerId = Guid.Empty;
        var query = GetCreateOrderQuery();
        const string ERROR_KEY = "CustomerId";
        const string ERROR_MESSAGE = "CustomerId cannot be empty.";
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
        var query = GetUpdateOrderQuery();
        var response = Response<Order>.FromSuccess(GetOrder()); ;
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
        var query = GetUpdateOrderQuery();
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
        var query = GetUpdateOrderQuery();
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
        var query = GetUpdateOrderQuery();
        query = query with { OrderId = Guid.Empty };
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
        var response = Response<Success>.FromSuccess(Success.Instance); ;
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnNotFound_WhenBadOrderIdIsChosen()
    {
        //Arrange
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await
            OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.NewGuid(),
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_KEY = "OrderId";
        const string ERROR_MESSAGE = "OrderId may not be empty.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await OrderEndpoints.DeleteOrder(
            _mockMediator.Object,
            Guid.NewGuid(),
            Guid.Empty,
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

    private static CreateOrderQuery GetCreateOrderQuery()
    {
        return new CreateOrderQuery(
            Guid.NewGuid(),
            new List<LineItemModel>
            {
                new LineItemModel(Guid.NewGuid(), 10),
                new LineItemModel(Guid.NewGuid(), 5)
            });
    }

    private static UpdateOrderQuery GetUpdateOrderQuery()
    {
        var orderId = Guid.NewGuid();
        return new UpdateOrderQuery(
            Guid.NewGuid(),
            orderId,
            DateTime.UtcNow,
            new List<LineItem>
            {
                new LineItem(orderId, Guid.NewGuid(), 10)
            }
);
    }

    private static IReadOnlyList<Order> GetEmptyOrdersList()
    {
        return new List<Order>();
    }

    private static IReadOnlyList<Order> GetOrderList(Guid orderId)
    {

        var orders = new List<Order>
        {
            new Order
            {
                CustomerId = orderId,
                OrderDateTimeUtc = DateTime.UtcNow,
            },
            new Order
            {
                CustomerId = orderId,
                OrderDateTimeUtc = DateTime.UtcNow,
            },
            new Order
            {
                CustomerId = orderId,
                OrderDateTimeUtc = DateTime.UtcNow,
            }
        };

        orders[0].LineItems.Add(new LineItem(orders[0].Id, Guid.NewGuid(), 10));
        orders[0].LineItems.Add(new LineItem(orders[0].Id, Guid.NewGuid(), 5));

        orders[1].LineItems.Add(new LineItem(orders[1].Id, Guid.NewGuid(), 10));
        orders[1].LineItems.Add(new LineItem(orders[1].Id, Guid.NewGuid(), 5));

        orders[2].LineItems.Add(new LineItem(orders[2].Id, Guid.NewGuid(), 10));
        orders[2].LineItems.Add(new LineItem(orders[2].Id, Guid.NewGuid(), 5));

        return orders;
    }

    private static Order GetOrder()
    {
        var order = new Order
        {
            CustomerId = Guid.NewGuid(),
            OrderDateTimeUtc = DateTime.UtcNow,
        };
        order.LineItems.Add(new LineItem(order.Id, Guid.NewGuid(), 10));
        order.LineItems.Add(new LineItem(order.Id, Guid.NewGuid(), 5));

        return order;
    }
    #endregion
}
