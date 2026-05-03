using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IEnumerable<Category>>;