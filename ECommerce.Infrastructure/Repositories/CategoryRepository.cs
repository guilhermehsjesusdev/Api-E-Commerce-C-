using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _ctx;

    public CategoryRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Category?> GetByIdAsync(Guid id) =>
        await _ctx.Categories.FindAsync(id);

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _ctx.Categories.ToListAsync();

    public async Task AddAsync(Category category)
    {
        await _ctx.Categories.AddAsync(category);
        await _ctx.SaveChangesAsync();
    }
}