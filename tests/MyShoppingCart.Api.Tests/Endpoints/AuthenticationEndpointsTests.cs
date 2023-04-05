namespace MyShoppingCart.Api.Tests.Endpoints;

public class AuthenticationEndpointsTests
{
	private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
	private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

	[Fact]
	public async Task GetToken_ShouldReturnToken_WhenCustomerIdIsValid()
	{
        //Arrange
        const string token = "VALID_TOKEN";
        var customerId = new Guid("50EAD54E-CFEC-44CA-9325-FCE68F6E85C3");
        var query = new JwtTokenQuery(customerId);
        var response = Response<string>.FromSuccess(token); ;
        SetupMediator(query, response);

        //Act
        var httpResult = (ContentHttpResult) await AuthenticationEndpoints.GetToken(
            _mockMediator.Object,
            customerId,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.ResponseContent.Should().NotBeNull().And.Be(token);
	}

    [Fact]
    public async Task GetToken_ShouldReturnUnauthorized_WhenCustomerIdIsInvalid()
    {
        //Arrange
        var customerId = new Guid("3D46089B-892D-423C-8D42-1CF555616208");
        var query = new JwtTokenQuery(customerId);
        var response = Response<string>.FromUnauthorized(); ;
        SetupMediator(query, response);

        //Act
        var httpResult = (UnauthorizedHttpResult)await AuthenticationEndpoints.GetToken(
            _mockMediator.Object,
            customerId,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task GetToken_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        const string ERROR_MESSAGE = "'Customer Id' must not be empty.";
        const string ERROR_KEY = "CustomerId";
        var customerId = Guid.Empty;
        var query = new JwtTokenQuery(customerId);
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<string>.FromValidationFailure(_validationErrors);
        SetupMediator(query, response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.GetToken(
            _mockMediator.Object,
            customerId,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    [Fact]
    public async Task GetToken_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var customerId = new Guid("3D46089B-892D-423C-8D42-1CF555616208");
        var query = new JwtTokenQuery(customerId);
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<string>.FromErrorList(_errors);
        SetupMediator(query, response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.GetToken(
            _mockMediator.Object,
            customerId,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    private void SetupMediator(JwtTokenQuery query, Response<string> response)
    {
        _mockMediator
            .Setup(x => x.Send(query, _cancellationToken))
            .ReturnsAsync(response);
    }
}
