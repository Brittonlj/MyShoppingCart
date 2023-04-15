namespace MyShoppingCart.Application.Categories;

public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<IReadOnlyList<Category>>>
{
    private readonly IRepository<Category> _categoryRepository;

    public GetCategoriesQueryHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = Guard.Against.Null(categoryRepository);
    }

    public async Task<Response<IReadOnlyList<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var categories = await _categoryRepository.ListAsync(cancellationToken);

        return categories;
    }
}
