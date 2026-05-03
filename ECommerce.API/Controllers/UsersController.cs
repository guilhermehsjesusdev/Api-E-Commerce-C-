using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserDto(user.Id, user.Email!, roles.FirstOrDefault() ?? ""));
        }

        return Ok(result);
    }

    [HttpPatch("{userId}/promote")]
    public async Task<IActionResult> Promote(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound(new { error = "Usuário não encontrado." });

        await _userManager.RemoveFromRoleAsync(user, "Customer");
        await _userManager.AddToRoleAsync(user, "Admin");

        return Ok(new { message = $"{user.Email} promovido a Admin." });
    }

    [HttpPatch("{userId}/demote")]
    public async Task<IActionResult> Demote(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound(new { error = "Usuário não encontrado." });

        await _userManager.RemoveFromRoleAsync(user, "Admin");
        await _userManager.AddToRoleAsync(user, "Customer");

        return Ok(new { message = $"{user.Email} rebaixado para Customer." });
    }
}

public record UserDto(string Id, string Email, string Role);