namespace MyShoppingCart.Application.Tests.Validators.Helpers;

public static class ProductsData
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = new Guid("452D3B0A-5FB2-43E5-A7BE-F4E49B930B1D"),
                Name = "Tennis Shoes",
                Description = "Some Tennis Shoes",
                Price = 10.00M,
                ImageUrl = null
            },
            new Product
            {
                Id = new Guid("516874DD-6CE7-4A5A-A2C0-E6FBA73DB4FC"),
                Name = "Running Shorts",
                Description = "Some Running Shorts",
                Price = 25.00M,
                ImageUrl = null
            },
            new Product
            {
                Id = new Guid("739FCB33-4A09-43D7-8CCD-949AA41053F1"),
                Name = "Baseball Cap",
                Description = "A Baseball Cap",
                Price = 15.00M,
                ImageUrl = null
            }
        };
    }
}
