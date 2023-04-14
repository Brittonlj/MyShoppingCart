namespace MyShoppingCart.Application.Categories;

public sealed record GetCategoriesQuery() :
    IQueryMany<Category>
{
}
