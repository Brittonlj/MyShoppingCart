namespace MyShoppingCart.Api.Tests.Helpers;

public static class SettingsHelper
{
    public static IOptionsSnapshot<MyShoppingCartSettings> GetMyShoppingCartSettings()
    {
        var settings = new MyShoppingCartSettings
        {
            DefaultPageSize = 20,
            DefaultPageSorting = new DefaultPageSorting
            {
                Customer = "LastName",
                Order = "OrderDateTimeUtc",
                Product = "Name"
            }
        };
        var mock = new Mock<IOptionsSnapshot<MyShoppingCartSettings>>();
        mock.Setup(x => x.Value).Returns(settings);
        return mock.Object;
    }
}
