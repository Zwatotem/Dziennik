using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models.Scoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Controllers;

public class Tasks(ApplicationDbContext context) : Controller
{
	private readonly ApplicationDbContext _context = context;

	// GET: Tasks
	public ActionResult Index(Guid courseId)
	{
		var tasks = _context.Tasks.Where(t => t.CourseId == courseId);
		ViewBag.courseId = courseId;
		return View(tasks);
	}

	// GET: Tasks/Details/5
	public ActionResult Details(Guid id)
	{
		return RedirectToAction(nameof(Marks.ByTask), nameof(Marks), new { taskId = id });
	}

	// GET: Tasks/Create
	[Authorize(Roles = "Admin, Teacher")]
	public ActionResult Create(Guid courseId)
	{
		var course = _context.Courses
		                     .Include(course => course.Program)
		                     .Find(courseId);
		ViewBag.CourseId = course.Id;
		return View();
	}

	// POST: Tasks/Create
	[Authorize(Roles = "Admin, Teacher")]
	[HttpPost]
	public async Task<ActionResult> Create(ClassTask task)
	{
		var course = _context.Courses
		                     .Include(course => course.Program)
		                     .Include(course => course.Teacher)
		                     .Find(task.CourseId);
		var taskMaster = course.Teacher;
		task.Course       = course;
		task.TaskMaster   = taskMaster;
		task.TaskMasterId = taskMaster.Id;
		_context.Tasks.Add(task);
		await _context.SaveChangesAsync();
		try
		{
			return RedirectToAction(nameof(Index), new {courseId = course.Id});
		}
		catch
		{
			return View();
		}
	}

	// GET: Tasks/Edit/5
	[Authorize(Roles = "Admin, Teacher")]
	public ActionResult Edit(Guid id)
	{
		return View();
	}

	// POST: Tasks/Edit/5
	[Authorize(Roles = "Admin, Teacher")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Edit(Guid id, IFormCollection collection)
	{
		try
		{
			return RedirectToAction(nameof(Index));
		}
		catch
		{
			return View();
		}
	}

	// GET: Tasks/Delete/5
	[Authorize(Roles = "Admin, Teacher")]
	public ActionResult Delete(Guid id)
	{
		return View();
	}

	// POST: Tasks/Delete/5
	[Authorize(Roles = "Admin, Teacher")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Delete(Guid id, IFormCollection collection)
	{
		try
		{
			return RedirectToAction(nameof(Index));
		}
		catch
		{
			return View();
		}
	}
}