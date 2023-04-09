using MediatR;
using MyShoppingCart.Application.PipelineBehaviors;

namespace MyShoppingCart.Application.Tests.PipelineBehaviors;

public class ValidationPipelineBehaviorTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly ValidationPipelineBehavior<GetCustomerQuery, Customer> _unitUnderTest;

    public ValidationPipelineBehaviorTests()
    {
        var validators = new List<IValidator<GetCustomerQuery>>
        {
            new GetCustomerQueryValidator()
        };

        _unitUnderTest = new ValidationPipelineBehavior<GetCustomerQuery, Customer>(validators);
    }

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldProcessNext_WhenSecurityStateIsNormal()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<Customer>>(Next);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.Be(DataProvider.GetCustomer());
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnValidationErrors_WhenCustomerIdIsEmpty()
    {
        //Arrange
        var request = new GetCustomerQuery(Guid.Empty);
        var next = new RequestHandlerDelegate<Response<Customer>>(Next);

        //Act
        var results = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        results.ValidationFailure.Should().NotBeNull();
        results.ValidationFailure.Results["CustomerId"][0]
            .Should().NotBeNull().And
            .Be("'Customer Id' must not be empty.");
    }

    #endregion

    #region Private Helpers

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<Response<Customer>> Next()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        return Response<Customer>.FromSuccess(DataProvider.GetCustomer());
    }

    #endregion
}
