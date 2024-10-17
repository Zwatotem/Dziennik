using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;

namespace Dziennik.Controllers;

public class CoursePrograms(ApplicationDbContext context) : Controller
{
	private readonly ApplicationDbContext _context = context;

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		return View(await _context.CoursePrograms.ToListAsync());
	}

	[HttpGet]
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null) return NotFound();

		var courseProgram = await _context.CoursePrograms
		                                  .Include(program => program.DedicatedTeachers)
		                                  .FindAsync(id);
		if (courseProgram == null) return NotFound();

		return View(courseProgram);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		await PrepareContext();
		return View();
	}

	private async Task PrepareContext()
	{
		ViewBag.DidacticLevels = Enum.GetValues<DidacticLevel>();
		ViewBag.Teachers       = await _context.Teachers.ToListAsync();
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateCourseProgram courseProgram)
	{
		var teachers = courseProgram
		              .DedicatedTeachers
		              .Select(async id => await _context.Teachers.FindAsync(id))
		              .Select(task => task.Result)
		              .ToList();
		if (teachers.Any(_ => false))
			ModelState.AddModelError(nameof(CreateCourseProgram.DedicatedTeachers), "Teacher not found");
		if (ModelState.IsValid)
		{
			_context.CoursePrograms.Add(new CourseProgram
			{
				Description       = courseProgram.Description,
				DidacticLevel     = courseProgram.DidacticLevel,
				DedicatedTeachers = teachers
			});
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		await PrepareContext();
		return View(courseProgram);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null) return NotFound();

		var courseProgram = await _context
		                         .CoursePrograms
		                         .Include(program => program.DedicatedTeachers)
		                         .FindAsync(id);
		if (courseProgram == null) return NotFound();
		var editCourseProgram = new EditCourseProgram(
			courseProgram.Id,
			courseProgram.Description,
			courseProgram.DidacticLevel,
			courseProgram.DedicatedTeachers.Select(teacher => teacher.Id).ToList()
		);
		await PrepareContext();
		return View(editCourseProgram);
	}

	// POST: CoursePrograms/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, EditCourseProgram courseProgramChanges)
	{
		if (id != courseProgramChanges.Id) return NotFound();
		var teachers = await courseProgramChanges
		                    .DedicatedTeachers
		                   ?.Select(async id =>
			                     await _context
			                          .Teachers
			                          .FindAsync(id))
		                    .ListOfAwaited();
		if (!ModelState.IsValid)
		{
			await PrepareContext();
			return View(courseProgramChanges);
		}

		try
		{
			var courseProgram = await _context.CoursePrograms
			                                  .Include(program => program.DedicatedTeachers)
			                                  .FindAsync(id);
			courseProgram.Description       = courseProgramChanges.Description;
			courseProgram.DidacticLevel     = courseProgramChanges.DidacticLevel;
			courseProgram.DedicatedTeachers = teachers.ToList();
			_context.Update(courseProgram);
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!CourseProgramExists(courseProgramChanges.Id)) return NotFound();

			throw;
		}

		return RedirectToAction(nameof(Index));
	}

	// GET: CoursePrograms/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null) return NotFound();

		var courseProgram = await _context.CoursePrograms
		                                  .FindAsync(id);
		if (courseProgram == null) return NotFound();

		return View(courseProgram);
	}

	// POST: CoursePrograms/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var courseProgram = await _context.CoursePrograms.FindAsync(id);
		if (courseProgram != null) _context.CoursePrograms.Remove(courseProgram);

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool CourseProgramExists(Guid id)
	{
		return _context.CoursePrograms.Any(e => e.Id == id);
	}
}