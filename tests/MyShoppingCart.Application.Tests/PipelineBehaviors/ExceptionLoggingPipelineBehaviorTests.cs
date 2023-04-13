using MediatR;
using Microsoft.Extensions.Logging;
using MyShoppingCart.Application.PipelineBehaviors;

namespace MyShoppingCart.Application.Tests.PipelineBehaviors;

public class ExceptionLoggingPipelineBehaviorTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<ILogger<GetCustomerQuery>> _mockLogger;
    private readonly ExceptionLoggingPipelineBehavior<GetCustomerQuery, CustomerModel> _unitUnderTest;

    public ExceptionLoggingPipelineBehaviorTests()
    {
        _mockLogger = new Mock<ILogger<GetCustomerQuery>>();

        _unitUnderTest = new ExceptionLoggingPipelineBehavior<GetCustomerQuery, CustomerModel>(_mockLogger.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldProcessNext_WhenNoExceptionIsThrown()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<CustomerModel>>(Next);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.BeEquivalentTo(DataProvider.GetCustomerModel());
    }

    #endregion

    #region Exception thrown

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<CustomerModel>>(NextThrow);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.ErrorList.Should().NotBeNull();
        result.ErrorList[0].Message.Should().NotBeNull().And.Be("Oops");
    }


    #endregion

    #region Private Helpers

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<Response<CustomerModel>> Next()
    {
        return Response<CustomerModel>.FromSuccess(DataProvider.GetCustomerModel());
    }

    private async Task<Response<CustomerModel>> NextThrow()
    {
        throw new ApplicationException("Oops");
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    #endregion

}
