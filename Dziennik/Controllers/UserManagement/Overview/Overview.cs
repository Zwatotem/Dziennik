using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Controllers.UserManagement.Overview;

[Authorize(Roles = "Admin")]
[Route("UserManagement/Overview/[action]")]
public class Overview(ApplicationDbContext context, UserManager<User> userManager) : Controller
{
	public async Task<IActionResult> Index()
	{
		var users = await context.Users
		                         .Include(u => u.Roles)
		                         .ToListAsync();
		return View("~/Views/UserManagement/Overview/Index.cshtml", users);
	}

	[HttpGet]
	public async Task<IActionResult> Edit([FromQuery] Guid userId)
	{
		var user = await context.Users
		                  .Include(user => user.Roles)
		                  .Include(user => user.RoleRequests)
		                   // Continue adding as new features get added
		                  .FindAsync(userId);
		if (user is null) return NotFound();
		return View("~/Views/UserManagement/Overview/Edit.cshtml", user);
	}

	[HttpPost]
	public async Task<IActionResult> Edit([FromQuery] Guid userId, [FromForm] DTUser user)
	{
		var userToUpdate = await context.Users
		                                .Include(user => user.Roles)
		                                .Include(user => user.RoleRequests)
		                                 // Continue adding as new features get added
		                                .FirstOrDefaultAsync(user => user.Id == userId);
		if (userToUpdate is null) return NotFound();
		userToUpdate.Names       = user?.Names?.Split(" ")?.ToList() ?? new();
		userToUpdate.Surname     = user?.Surname ?? userToUpdate.Surname;
		userToUpdate.PESEL       = user?.PESEL ?? userToUpdate.PESEL;
		userToUpdate.PhoneNumber = user?.PhoneNumber ?? userToUpdate.PhoneNumber;
		userToUpdate.Email       = user?.Email ?? userToUpdate.Email;
		userToUpdate.UserName    = user?.UserName ?? userToUpdate.UserName;
		// Continue adding as new features get added
		await context.SaveChangesAsync();
		return RedirectToAction("Index");
	}


	[HttpGet]
	public async Task<IActionResult> Delete([FromQuery] string userId)
	{
		var user = await userManager.FindByIdAsync(userId);
		if (user == null) return NotFound();

		user.Roles.Clear();
		var result = await userManager.DeleteAsync(user);
		if (result.Succeeded) return RedirectToAction("Index");

		foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

		return View("Error");
	}
}