using Mapster;
using MyShoppingCart.Application.Orders;

namespace MyShoppingCart.Application.Setup;

public static class MapsterConfig
{
    public static void AddMapsterConfig()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

        TypeAdapterConfig<CreateOrderQuery, Order>
            .NewConfig()
            .AfterMapping((src, result) =>
            {
                foreach (var item in src.LineItems)
                {
                    result.AddUpdateLineItem(item);
                }
            });

        TypeAdapterConfig<UpdateOrderQuery, Order>
            .NewConfig()
            .AfterMapping((src, result) =>
            {
                var lineItemsToDelete = result.LineItems
                    .Where(x => !src.LineItems.Any(y => x.Id == y.Id)).ToList();

                foreach (var item in lineItemsToDelete)
                {
                    result.RemoveLineItem(item.Id);
                }

                result.AddUpdateLineItemRange(src.LineItems);
            });
    }
}
