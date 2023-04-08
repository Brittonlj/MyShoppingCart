namespace MyShoppingCart.Application.Tests.Helpers;

public static class LineItemModelsData
{
    public static List<LineItemModel> GetLineItemModels()
    {
        return new List<LineItemModel>
        {
            new LineItemModel(new Guid("452D3B0A-5FB2-43E5-A7BE-F4E49B930B1D"), 10),
            new LineItemModel(new Guid("516874DD-6CE7-4A5A-A2C0-E6FBA73DB4FC"), 6),
            new LineItemModel(new Guid("739FCB33-4A09-43D7-8CCD-949AA41053F1"), 14)
        };
    }
}
