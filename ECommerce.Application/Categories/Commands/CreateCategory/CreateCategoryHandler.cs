using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _repo;

    public CreateCategoryHandler(ICategoryRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateCategoryCommand cmd, CancellationToken ct)
    {
        var category = new Category(cmd.Name);
        await _repo.AddAsync(category);
        return category.Id;
    }
}