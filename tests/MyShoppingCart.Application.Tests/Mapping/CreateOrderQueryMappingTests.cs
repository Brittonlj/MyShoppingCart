using MyShoppingCart.Application.Setup;

namespace MyShoppingCart.Application.Tests.Mapping;

public class CreateOrderQueryMappingTests
{
    private readonly IMapper _mapper = new Mapper();

    static CreateOrderQueryMappingTests()
    {
        MapsterConfig.AddMapsterConfig();
    }

    [Fact]
    public void Map_ShouldMapLineItems_WhenTheRequestHasLineItems()
    {
        //Arrange
        var request = DataHelper.GetCreateOrderQuery();

        //Act
        var order = _mapper.Map<Order>(request);

        //Assert
        order.Should().NotBeNull();
        order.CustomerId.Should().Be(request.CustomerId);
        order.LineItems.Should().BeEquivalentTo(request.LineItems);
        foreach(var lineItem in order.LineItems)
        {
            lineItem.OrderId.Should().Be(order.Id);
        }
    }
}
