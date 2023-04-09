using MediatR;
using Microsoft.Extensions.Logging;
using MyShoppingCart.Application.Configuration;
using MyShoppingCart.Application.PipelineBehaviors;
using MyShoppingCart.Application.Services;
using static System.Net.Mime.MediaTypeNames;

namespace MyShoppingCart.Application.Tests.PipelineBehaviors;

public class ExceptionLoggingPipelineBehaviorTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<ILogger<GetCustomerQuery>> _mockLogger;
    private readonly ExceptionLoggingPipelineBehavior<GetCustomerQuery, Customer> _unitUnderTest;

    public ExceptionLoggingPipelineBehaviorTests()
    {
        _mockLogger = new Mock<ILogger<GetCustomerQuery>>();

        _unitUnderTest = new ExceptionLoggingPipelineBehavior<GetCustomerQuery, Customer>(_mockLogger.Object);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldProcessNext_WhenNoExceptionIsThrown()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<Customer>>(Next);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.Be(DataProvider.GetCustomer());
        _mockLogger.Verify(x => x.LogError(It.IsAny<Exception>(), "An exception was caught."), Times.Never);
    }

    #endregion

    #region Exception thrown

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<Customer>>(NextThrow);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.ErrorList.Should().NotBeNull();
        result.ErrorList[0].Message.Should().NotBeNull().And.Be("Oops");
    }


    #endregion

    #region Private Helpers

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<Response<Customer>> Next()
    {
        return Response<Customer>.FromSuccess(DataProvider.GetCustomer());
    }

    private async Task<Response<Customer>> NextThrow()
    {
        throw new ApplicationException("Oops");
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    #endregion

}
