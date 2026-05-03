using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoryRepository _repo;

    public GetAllCategoriesHandler(ICategoryRepository repo) => _repo = repo;

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery q, CancellationToken ct)
        => await _repo.GetAllAsync();
}