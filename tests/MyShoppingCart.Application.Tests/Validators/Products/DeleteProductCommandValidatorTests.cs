﻿namespace MyShoppingCart.Application.Tests.Validators.Products;

public class DeleteProductCommandValidatorTests
{
    private readonly IValidator<DeleteProductCommand> _validator = new DeleteProductCommandValidator();
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Validate_ShouldReturnNoResults_WhenRequestIsValid()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.NewGuid());

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.Should().NotBeNull();
        results.Errors.Should().NotBeNull().And.BeEmpty();
    }

    #endregion

    #region ProductId

    [Fact]
    public async Task Validate_ShouldReturnResults_WhenProductIdIsEmpty()
    {
        //Arrange
        var request = new DeleteProductCommand(Guid.Empty);

        //Act
        var results = await _validator.ValidateAsync(request, _cancellationToken);

        //Assert
        results.AssertValidationErrors(
            nameof(DeleteProductCommand.ProductId),
            "'Product Id' must not be empty.");
    }

    #endregion
}
