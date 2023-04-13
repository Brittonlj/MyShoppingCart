namespace MyShoppingCart.Api.Tests.Endpoints;

public class AuthenticationEndpointsTests
{
	private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
	private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();


}
