using Dziennik.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Dziennik.Data;
using Dziennik.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Areas.Identity.Pages.Account.Manage;

public class RequestRole(UserManager<User> userManager, ApplicationDbContext context) : PageModel
{
	private readonly UserManager<User>    _userManager = userManager;
	private readonly ApplicationDbContext _context     = context;

	public List<RoleRequest> IssuedRequests { get; set; } = new();


	[BindProperty] public InputModel? Input { get; set; }

	public class InputModel
	{
		public required string RoleName { get; set; }
	}

	public async Task<IActionResult> OnGetAsync()
	{
		var user = await _userManager.GetUserAsync(User);
		if (user is null) return RedirectToPage("/Account/Login");

		IssuedRequests = _context
		                .RoleRequests
		                .Where(rr => rr.User.Id == user.Id)
		                .Include(rr => rr.RequestedRole)
		                .ToList();
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid) return Page();

		var user = await _userManager.GetUserAsync(User);
		if (user is null) return RedirectToPage("/Account/Login");
		if (Input is null) return Page();

		var roleRequest = new RoleRequest
		{
			User          = user,
			RequestedRole = Role.FromString(Input.RoleName, user),
			Status        = RoleRequest.RequestStatus.Pending,
			RequestDate   = DateTime.Now
		};
		_context.RoleRequests.Add(roleRequest);
		await _context.SaveChangesAsync();

		return RedirectToPage();
	}
}