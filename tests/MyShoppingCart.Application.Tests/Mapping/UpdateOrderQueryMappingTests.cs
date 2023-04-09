using MyShoppingCart.Application.Setup;

namespace MyShoppingCart.Application.Tests.Mapping;

public class UpdateOrderQueryMappingTests
{
    private readonly IMapper _mapper = new Mapper();

    static UpdateOrderQueryMappingTests()
    {
        MapsterConfig.AddMapsterConfig();
    }

    [Fact]
    public void Map_ShouldMapLineItems_WhenTheRequestHasLineItems()
    {
        //Arrange
        var request = DataHelper.GetUpdateOrderQuery();
        var order = DataHelper.GetOrder();
        //Act
        _mapper.Map(request, order);

        //Assert
        order.Should().NotBeNull();
        order.Id.Should().Be(request.OrderId);
        order.CustomerId.Should().Be(request.CustomerId);
        order.LineItems.Should().BeEquivalentTo(request.LineItems);
        foreach (var lineItem in order.LineItems)
        {
            lineItem.OrderId.Should().Be(order.Id);
        }
    }

    [Fact]
    public void Map_ShouldMapLineItems_WhenTheRequestDeletesLineItems()
    {
        //Arrange
        var request = DataHelper.GetUpdateOrderQuery(2);

        var order = DataHelper.GetOrder(3);
        //Act
        _mapper.Map(request, order);

        //Assert
        order.Should().NotBeNull();
        order.Id.Should().Be(request.OrderId);
        order.CustomerId.Should().Be(request.CustomerId);
        order.LineItems.Should().BeEquivalentTo(request.LineItems);
        foreach (var lineItem in order.LineItems)
        {
            lineItem.OrderId.Should().Be(order.Id);
        }
    }

    [Fact]
    public void Map_ShouldMapLineItems_WhenTheRequestAddsLineItems()
    {
        //Arrange
        var request = DataHelper.GetUpdateOrderQuery(3);

        var order = DataHelper.GetOrder(2);
        //Act
        _mapper.Map(request, order);

        //Assert
        order.Should().NotBeNull();
        order.Id.Should().Be(request.OrderId);
        order.CustomerId.Should().Be(request.CustomerId);
        order.LineItems.Should().BeEquivalentTo(request.LineItems);
        foreach (var lineItem in order.LineItems)
        {
            lineItem.OrderId.Should().Be(order.Id);
        }
    }

}
