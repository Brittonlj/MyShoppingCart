using Mapster;
using MyShoppingCart.Application.Orders;
using MyShoppingCart.Application.Products;

namespace MyShoppingCart.Application.Setup;

public static class MapsterConfig
{
    public static void AddMapsterConfig()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

        TypeAdapterConfig<CreateProductQuery, Product>
            .NewConfig()
            .AfterMapping((src, result) =>
            {
                foreach (var category in src.Categories)
                {
                    result.AddUpdate(category);
                }
            });


        TypeAdapterConfig<UpdateProductQuery, Product>
            .NewConfig()
            .AfterMapping((src, result) =>
            {
                var itemsToDelete = result.Categories
                    .Where(x => !src.Categories.Any(y => x.Id == y.Id)).ToList();

                foreach (var item in itemsToDelete)
                {
                    result.Remove(item);
                }

                result.AddUpdateRange(src.Categories);
            });


        TypeAdapterConfig<CreateOrderQuery, Order>
            .NewConfig()
            .AfterMapping((src, result) =>
            {
                foreach (var item in src.LineItems)
                {
                    result.AddUpdate(item);
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
                    result.Remove(item.Id);
                }

                result.AddUpdateRange(src.LineItems);
            });
    }
}
