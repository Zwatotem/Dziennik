using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;
using Dziennik.Models.Roles;
using Dziennik.Models.Scheduling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Dziennik.Controllers;

public class Courses(ApplicationDbContext context, UserManager<User> userManager) : Controller
{
	private readonly UserManager<User>    _userManager = userManager;
	private readonly ApplicationDbContext _context     = context;

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var allCourses = await _context.CoursesWithDependencies.ToListAsync();
		allCourses = allCourses
		            .OrderBy(course => course.Group == null)
		            .ThenByDescending(course => course.Group?.DidacticCycle.Contains(DateTime.Now))
		            .ThenByDescending(course => course.Group?.DidacticCycle.StartDate)
		            .ToList();
		var loggedUser = await _userManager.GetUserAsync(User);
		loggedUser = await _context.Users
		                           .Include(user => user.Roles)
		                           .FindAsync(loggedUser.Id);
		var coursesForUser = loggedUser
		                    .Roles
		                    .SelectMany(role => role.RelevantCourses(allCourses));
		if (User.IsInRole("Student"))
		{
			var userAsStudent = loggedUser.Roles.OfType<Student>().FirstOrDefault();
			var averages = coursesForUser.Select(course => (course.Id, course.Tasks.Select(task =>
				                                                                  (task.Marks.FirstOrDefault(mark =>
						                                                                   mark.RecieverId == userAsStudent.Id)
					                                                                  .Value *
				                                                                   task.Weight))
			                                                                 .Sum() /
			                                                           course.Tasks.Select(task => task.Weight).Sum()))
			                             .ToDictionary(el => el.Id, el => el.Item2);
			ViewBag.AverageFor = averages;
		}

		return View(coursesForUser);
	}

	[HttpGet]
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null) return NotFound();

		var course = await _context.CoursesWithDependencies
		                           .FindAsync(id);
		if (course == null) return NotFound();

		return View(course);
	}

	private async Task SetupContext()
	{
		ViewBag.CoursePrograms = await _context.CoursePrograms.ToListAsync();
		ViewBag.Groups         = await _context.Groups.Include(group => group.DidacticCycle).ToListAsync();
		ViewBag.Teachers       = await _context.IndividualRoles.OfType<Teacher>().ToListAsync();
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> Create()
	{
		await SetupContext();
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Course course)
	{
		course.Program = await _context.CoursePrograms
		                               .Include(program => program.DedicatedTeachers)
		                               .FindAsync(course.ProgramId);
		course.Group = await _context.Groups.FindAsync(course.GroupId);
		course.Teacher = await _context
		                      .Teachers
		                      .FindAsync(course.TeacherId);

		if (course.Program is null) ModelState.AddModelError(nameof(course.ProgramId), "Invalid course program");
		if (course.Group__ is null) ModelState.AddModelError(nameof(course.GroupId),   "Invalid group");
		if (course.Teacher is null) ModelState.AddModelError(nameof(course.TeacherId), "Invalid teacher");

		if (course.Program.DidacticLevel != course.Group.DidacticLevel)
			ModelState.AddModelError(nameof(course.GroupId),
				"Group must be in the same didactic level as the course program");
		if (!course.Program.DedicatedTeachers.Contains(course.Teacher))
			ModelState.AddModelError(nameof(course.TeacherId), "Teacher must be dedicated to the course program");

		if (ModelState.IsValid)
		{
			course.RecurringPlan = new RecurringPlan();
			course.DetailedPlan  = new DetailedPlan();
			_context.Add(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		await SetupContext();
		return View(course);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null) return NotFound();

		var course = await _context.CoursesWithDependencies.FirstOrDefaultAsync(course => course.Id == id);
		if (course == null) return NotFound();
		await SetupContext();
		return View(course);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProgramId,TeacherId,GroupId")] Course course)
	{
		if (id != course.Id) return NotFound();

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(course);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CourseExists(course.Id))
					return NotFound();
				else
					throw;
			}

			return RedirectToAction(nameof(Index));
		}

		await SetupContext();
		return View(course);
	}

	[HttpGet]
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null) return NotFound();

		var course = await _context.Courses
		                           .FindAsync(id);
		if (course == null) return NotFound();

		return View(course);
	}

	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var course = await _context.Courses.FindAsync(id);
		_context.Courses.Remove(course);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool CourseExists(Guid id)
	{
		return _context.Courses.Any(e => e.Id == id);
	}

	[HttpGet]
	public async Task<IActionResult> Schedule(Guid courseId)
	{
		var course = await _context
		                  .Courses
		                  .Include(course => course.RecurringPlan)
		                  .FindAsync(courseId);
	getId:
		var planId = course.RecurringPlan?.Id;
		if (planId is null)
		{
			var plan = new RecurringPlan();
			course.RecurringPlan = plan;
			_context.RecurringPlans.Add(plan);
			_context.Update(course);
			await _context.SaveChangesAsync();
			goto getId;
		}

		return RedirectToAction(nameof(Scheduling.Index), nameof(Scheduling), new { id = planId });
	}
}