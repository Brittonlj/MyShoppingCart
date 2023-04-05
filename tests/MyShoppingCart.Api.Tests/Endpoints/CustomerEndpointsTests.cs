using MyShoppingCart.Application.Customers;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class CustomerEndpointsTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly CancellationToken _cancellationToken;
    private readonly IOptionsSnapshot<MyShoppingCartSettings> _settings;

    public CustomerEndpointsTests()
    {
        _settings = SettingsHelper.GetMyShoppingCartSettings();

        _cancellationToken = new CancellationToken();
        _mockMediator = new Mock<IMediator>();
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var response = Response<IReadOnlyList<Customer>>.FromSuccess(CustomerHelper.GetEmptyCustomersList()); ;
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), _cancellationToken))
            .ReturnsAsync(response);

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
        var response = ResponseHelper.GetSampleErrorResponse<IReadOnlyList<Customer>>();
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), _cancellationToken))
            .ReturnsAsync(response);

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
    public async Task GetAllCustomers_ShouldReturnValidationErrorList_WhenInvalidSortOrderIsProvided()
    {
        //Arrange
        const string KEY = "SortColumn";
        const string ERROR_MESSAGE = "InvalidSortColumn is an invalid value for SortColumn";
        var response = ResponseHelper
            .GetSampleValidationErrorResponse<IReadOnlyList<Customer>>(KEY, ERROR_MESSAGE);
        _mockMediator
            .Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), _cancellationToken))
            .ReturnsAsync(response);

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
        httpResult.AssertCommonValidationErrorConditions(KEY, ERROR_MESSAGE);
    }
}
