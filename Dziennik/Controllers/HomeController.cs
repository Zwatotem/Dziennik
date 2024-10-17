using System.Diagnostics;
using Dziennik.Data;
using Dziennik.Linq;
using Microsoft.AspNetCore.Mvc;
using Dziennik.Models;
using Dziennik.Models.Roles;
using Dziennik.Models.Scheduling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Controllers;

public class HomeController(UserManager<User> userManager, ApplicationDbContext context) : Controller
{
	private readonly UserManager<User>    _userManager = userManager;
	private readonly ApplicationDbContext _context     = context;

	public async Task<IActionResult> Index()
	{
		var user = await _userManager.GetUserAsync(HttpContext.User);
		if (user is null)
		{
			return RedirectToPage("/Account/Login", new { area = "Identity" });
		}

		user = await _context.Users
		                     .Include(u => u.Roles)
		                     .ThenInclude(role=>((Student)role).Group)
		                     .FindAsync(user.Id);
		List<Event> allEvents = await _context
		                             .Events
		                             .AsNoTracking()
		                             .Include(e => e.Course)
		                             .ThenInclude(course => course.Teacher)
		                             .Include(e => e.Course)
		                             .ThenInclude(course => course.Group)
		                             .Include(@event => @event.Presence)
		                             .ToListAsync();
		var events = user.Roles.SelectMany(
			role => role.RelevantEvents(allEvents));
		var currentEvents     = events.Where(e => e.StartDate < DateTime.Now && e.EndDate > DateTime.Now);
		var upcomingEvents    = events.Where(e => e.StartDate > DateTime.Now).OrderBy(e => e.StartDate).ToList();
		var theEventToDisplay = currentEvents.FirstOrDefault() ?? upcomingEvents.FirstOrDefault();
		// Load all the data for the event to display

		if (theEventToDisplay is not null)
		{
			theEventToDisplay.Course = await _context.Courses
			                                         .Include(c => c.Program)
			                                         .Include(c => c.Teacher)
			                                         .Include(c => c.RecurringPlan)
			                                         .Include(c => c.DetailedPlan)
			                                         .Include(c => c.Group)
			                                         .ThenInclude(group => group.DidacticCycle)
			                                         .Include(c => c.Group)
			                                         .ThenInclude(group => group.Students)
			                                         .FindAsync(theEventToDisplay.Course.Id);
			theEventToDisplay.Presence = await _context.PresenceLogs
			                                           .Include(pl => pl.Students)
			                                           .ThenInclude(pr => pr.Student)
			                                           .FindAsync(theEventToDisplay.Presence.Id);
			if (!theEventToDisplay.Presence.Students.Any())
			{
				theEventToDisplay.Presence.Students = theEventToDisplay
				                                     .Course
				                                     .Group
				                                     .Students
				                                     .OrderBy(student => student.Owner.Surname)
				                                     .Select(student => new PresenceRecord()
				                                      {
					                                      Presence      = PresenceLog.PresenceType.Unexcused,
					                                      PresenceLog   = theEventToDisplay.Presence,
					                                      Student       = student,
					                                      StudentId     = student.Id,
					                                      PresenceLogId = theEventToDisplay.Presence.Id,
				                                      })
				                                     .ToList();
				_context.PresenceRecords.AddRange(theEventToDisplay.Presence.Students);
				await _context.SaveChangesAsync();
			}

			theEventToDisplay.Presence.TeacherId = theEventToDisplay.Course.Teacher.Id;
		}

		return View(new HomeScreen
		{
			CurrentEvent   = theEventToDisplay,
			UpcomingEvents = upcomingEvents
		});
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[Authorize(Roles = "Teacher")]
	[HttpPost]
	public async Task<IActionResult> SavePresence(PresenceLog presence)
	{
		var user = await _userManager.GetUserAsync(HttpContext.User);
		user = await _context.Users.Include(user => user.Roles).FindAsync(user.Id);
		if (!user.Roles.Select(role => role.Id).Contains(presence.TeacherId))
			return Unauthorized();
		var realPresence = await _context.PresenceLogs
		                                 .Include(pl => pl.Students)
		                                 .ThenInclude(pr => pr.Student)
		                                 .FindAsync(presence.Id);
		var realPresenceRecords = realPresence.Students;
		foreach (var record in realPresenceRecords)
		{
			var correspondingRecord = presence.Students.FirstOrDefault(pr => pr.StudentId == record.StudentId);
			record.Presence = correspondingRecord.Presence;
		}

		realPresence.Teacher   = presence.Teacher;
		realPresence.TeacherId = presence.TeacherId;
		_context.PresenceLogs.Update(realPresence);
		_context.PresenceRecords.UpdateRange(realPresence.Students);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
}