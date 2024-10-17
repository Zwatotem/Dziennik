using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Dziennik.Models.RoleRequest.RequestStatus;

namespace Dziennik.Controllers.UserManagement;

[Authorize(Roles = "Admin")]
[Route("/UserManagement/RoleRequests/[action]")]
public class RoleRequests(ApplicationDbContext context, UserManager<User> userManager) : Controller
{
	[Route("/UserManagement/RoleRequests")]
	public IActionResult Index()
	{
		var requests = context
		              .RoleRequests
		              .Where(rr => rr.Status == Pending)
		              .Include(rr => rr.RequestedRole)
		              .Include(rr => rr.User)
		              .ToList();
		return View("~/Views/UserManagement/RoleRequests/Index.cshtml", requests);
	}

	[HttpGet]
	public async Task<IActionResult> Accept([FromQuery] Guid roleRequestId)
	{
		var request = await context.RoleRequests
		                     .Include(rr => rr.User)
		                     .Include(rr => rr.RequestedRole)
		                     .FindAsync(roleRequestId);
		if (request?.Status != Pending)
			return BadRequest();
		request.Status       = Approved;
		request.DecisionDate = DateTime.Now;

		await request.User.AddRole(request.RequestedRole, userManager);
		await context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public IActionResult Reject([FromQuery] Guid roleRequestId)
	{
		var request = context.RoleRequests
		                     .Include(rr => rr.User)
		                     .Include(rr => rr.RequestedRole)
		                     .FirstOrDefault(rr => rr.Id == roleRequestId);
		if (request?.Status != Pending)
			return BadRequest();
		request.Status       = Rejected;
		request.DecisionDate = DateTime.Now;
		context.SaveChanges();
		return RedirectToAction(nameof(Index));
	}
}