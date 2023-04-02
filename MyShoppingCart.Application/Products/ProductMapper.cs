namespace MyShoppingCart.Application.Products;

public static class ProductMapper
{
    public static Product ToEntity(this CreateProductQuery other)
    {
        return new Product
        {
            Name = other.Name,
            Description = other.Description,
            Price = other.Price,
            ImageUrl = other.ImageUrl
        };
    }

    public static Product ToEntity(this ProductModel other)
    {
        return new Product
        {
            Id = other.Id,
            Name = other.Name,
            Description = other.Description,
            Price = other.Price,
            ImageUrl = other.ImageUrl
        };
    }

    public static ProductModel ToModel(this Product other)
    {
        return new ProductModel(
            other.Id,
            other.Name,
            other.Description,
            other.Price,
            other.ImageUrl);
    }

    public static List<ProductModel> ToModels(this IEnumerable<Product> others)
    {
        return others.Select(x => x.ToModel()).ToList();
    }
}
