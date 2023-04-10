using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Authentication;

public sealed class JwtTokenQueryHandlerTests
{
    private const string TOKEN = "TEST123";
    private readonly List<SecurityClaim> _validClaims;
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    public JwtTokenQueryHandlerTests()
    {
        _validClaims = DataProvider.GetClaims();
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenAllParametersAreValid()
    {
        //Arrange
        var request = new JwtTokenQuery(DataProvider.DefaultCustomerId);
        var claimsRepository = GetClaimsRepository(_validClaims);
        var jwtTokenService = GetJwtTokenService(TOKEN);
        var jwtTokenQueryHandler = new JwtTokenQueryHandler(claimsRepository, jwtTokenService);

        //Act
        var results = await jwtTokenQueryHandler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().Be($"Bearer {TOKEN}");
    }

    #endregion

    #region Constructor Guard Clauses

    [Fact]
    public void Constructor_ShouldReturnUnauthorized_WhenNoSecurityClaimsFound()
    {
        //Arrange
        var tokenService = GetJwtTokenService(TOKEN);

        //Act
        Action action = () => new JwtTokenQueryHandler(null!, tokenService);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenTokenServiceIsNull()
    {
        //Arrange
        var claimsRepository = GetClaimsRepository(_validClaims);

        //Act
        Action action = () => new JwtTokenQueryHandler(claimsRepository, null!);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Handle

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenNoCLaimsAreFound()
    {
        //Arrange
        var noClaims = new List<SecurityClaim>();
        var claimsRepository = GetClaimsRepository(noClaims);
        var jwtTokenService = GetJwtTokenService(TOKEN);
        var jwtTokenQueryHandler = new JwtTokenQueryHandler(claimsRepository, jwtTokenService);

        var request = new JwtTokenQuery(DataProvider.DefaultCustomerId);

        //Act
        var results = await jwtTokenQueryHandler.Handle(request, _cancellationToken);

        //Assert
        results.Unauthorized.Should().NotBeNull();
    }

    #endregion

    #region Private Helper

    private IRepository<SecurityClaim> GetClaimsRepository(List<SecurityClaim> claims)
    {
        var mockSecurityClaimRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimRepository.Setup(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(claims);
        return mockSecurityClaimRepository.Object;
    }

    private IJwtTokenService GetJwtTokenService(string token)
    {
        var mockJwtTokenService = new Mock<IJwtTokenService>();
        mockJwtTokenService.Setup(x => x.GenerateToken(_validClaims)).Returns(token);
        return mockJwtTokenService.Object;
    }

    #endregion
}
