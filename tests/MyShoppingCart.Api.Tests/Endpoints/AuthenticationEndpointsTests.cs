using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Api.Tests.Endpoints;

public class AuthenticationEndpointsTests
{
	private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
	private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Dictionary<string, string[]> _validationErrors =
        new Dictionary<string, string[]>();
    private readonly ErrorList _errors = new ErrorList();

    #region Login

    [Fact]
    public async Task Login_ShouldReturnCustomers_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "somePassword");
        var response = Response<AuthenticationResponseModel>.FromSuccess(DataProvider.GetAuthenticationResponseModel());
        SetupMediator<LoginQuery>(response);

        //Act
        var httpResult = (Ok<AuthenticationResponseModel>)await AuthenticationEndpoints.Login(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task Login_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "somePassword");
        var response = Response<AuthenticationResponseModel>.FromNotFound();
        SetupMediator<LoginQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await AuthenticationEndpoints.Login(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "somePassword");
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<AuthenticationResponseModel>.FromErrorList(_errors);
        SetupMediator<LoginQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.Login(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task Login_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new LoginQuery("fred.flintstone", "");
        const string ERROR_KEY = "Password";
        const string ERROR_MESSAGE = "'Password' is requrired.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<AuthenticationResponseModel>.FromValidationFailure(_validationErrors);
        SetupMediator<LoginQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.Login(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Register

    [Fact]
    public async Task Register_ShouldReturnCustomers_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var response = Response<CustomerModel>.FromSuccess(DataProvider.GetCustomerModel());
        SetupMediator<RegisterQuery>(response);

        //Act
        var httpResult = (Ok<CustomerModel>)await AuthenticationEndpoints.Register(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
        httpResult.Value.Should().NotBeNull().And.BeEquivalentTo(response.Success);
    }

    [Fact]
    public async Task Register_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        var response = Response<CustomerModel>.FromNotFound();
        SetupMediator<RegisterQuery>(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await AuthenticationEndpoints.Register(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Register_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery();
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<CustomerModel>.FromErrorList(_errors);
        SetupMediator<RegisterQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.Register(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task Register_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = QueryProvider.GetRegisterQuery() with { Password = "" };
        const string ERROR_KEY = "Password";
        const string ERROR_MESSAGE = "'Password' is requrired.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<CustomerModel>.FromValidationFailure(_validationErrors);
        SetupMediator<RegisterQuery>(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.Register(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region ChangePassword

    [Fact]
    public async Task ChangePassword_ShouldReturnCustomers_WhenValidParametersAreChosen()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        var response = Response<Success>.FromSuccess(Success.Instance);
        SetupMediator(response);

        //Act
        var httpResult = (Ok)await AuthenticationEndpoints.ChangePassword(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnNoCustomers_WhenBadParametersAreChosen()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        var response = Response<Success>.FromNotFound();
        SetupMediator(response);

        //Act
        var httpResult = (Microsoft.AspNetCore.Http.HttpResults.NotFound)await AuthenticationEndpoints.ChangePassword(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.Should().NotBeNull();
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnErrorList_WhenErrorsHappen()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        _errors.Add(new Error("Exception", "An error has occured"));
        var response = Response<Success>.FromErrorList(_errors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.ChangePassword(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonErrorConditions(response.ErrorList);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnHttpValidationProblemDetails_WhenValidationFails()
    {
        //Arrange
        var request = new ChangePasswordCommand(DataProvider.DefaultCustomerId, "oldPassword", "newPassword");
        const string ERROR_KEY = "Password";
        const string ERROR_MESSAGE = "'Password' is requrired.";
        _validationErrors.Add(ERROR_KEY, new string[] { ERROR_MESSAGE });
        var response = Response<Success>.FromValidationFailure(_validationErrors);
        SetupMediator(response);

        //Act
        var httpResult = (ProblemHttpResult)await AuthenticationEndpoints.ChangePassword(
            _mockMediator.Object,
            request,
            _cancellationToken);

        //Assert
        httpResult.AssertCommonValidationErrorConditions(ERROR_KEY, ERROR_MESSAGE);
    }

    #endregion

    #region Private Helpers

    private void SetupMediator(Response<Success> response)
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<ChangePasswordCommand>(), _cancellationToken))
            .ReturnsAsync(response);
    }

    private void SetupMediator<T>(Response<AuthenticationResponseModel> response)
        where T : class, IQuery<AuthenticationResponseModel>
    {
        _mockMediator
            .Setup(x => x.Send(It.IsAny<T>(), _cancellationToken))
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
