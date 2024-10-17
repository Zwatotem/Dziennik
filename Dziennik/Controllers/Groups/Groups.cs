using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;
using Microsoft.Data.SqlClient;

namespace Dziennik.Controllers.Groups;

public class Groups : Controller
{
	private readonly ApplicationDbContext _context;

	public Groups(ApplicationDbContext context)
	{
		_context = context;
	}

	protected async Task SetContext()
	{
		ViewBag.DidacticLevels = Enum.GetValues<DidacticLevel>();
		ViewBag.DidacticCycles = await _context.DidacticCycles.ToListAsync();
		ViewBag.Supervisors    = await _context.Teachers.ToListAsync();
		ViewBag.Students = await _context
		                        .Students
		                        .Where(student => student.Group == null)
		                        .ToListAsync();
	}

	// GET: Groups
	public async Task<IActionResult> Index()
	{
		return View(
			await _context
			     .Groups
			     .Include(group => group.DidacticCycle)
			     .Include(group => group.Supervisor)
			     .ToListAsync());
	}

	// GET: Groups/Details/5
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null) return NotFound();

		var @group = await _context.Groups
		                           .Include(group => group.DidacticCycle)
		                           .Include(group => group.Supervisor)
		                           .Include(group => group.Students)
		                           .FindAsync(id);
		if (@group == null) return NotFound();

		return View(@group);
	}

	// GET: Groups/Create
	public async Task<IActionResult> Create()
	{
		// Pass context data to the view
		await SetContext();
		return View();
	}


	// POST: Groups/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Name,DidacticLevel,DidacticCycle,SupervisorId")] Group @group)
	{
		@group.Supervisor = await _context
		                         .Teachers
		                         .FindAsync(@group.SupervisorId);
		@group.DidacticCycle =
			(await _context.DidacticCycles.FindAsync(group.DidacticCycle.Id))!;

		if (@group.Supervisor is null)
			ModelState.AddModelError(nameof(@group.SupervisorId), "Supervisor must be a valid teacher");
		if (@group.DidacticCycle == null!)
			ModelState.AddModelError(nameof(@group.DidacticCycle.Id), "Didactic cycle must be a valid period");
		if (ModelState.IsValid)
		{
			try
			{
				var cycle = await _context
				                 .DidacticCycles
				                 .FindAsync(group.DidacticCycle.Id)
				         ?? throw new KeyNotFoundException();

				@group.DidacticCycle = cycle;

				_context.Add(@group);
				cycle.Groups.Add(@group);
				await _context.SaveChangesAsync();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			catch (DbUpdateException sqlException) when (sqlException.InnerException is SqlException innerException
			                                          && innerException.Number == 2601)
			{
				ModelState.AddModelError("Name", "Group with this name already exists.");
				await SetContext();
				return View(@group);
			}

			return RedirectToAction(nameof(Index));
		}

		await SetContext();

		return View(@group);
	}

	// GET: Groups/Edit/5
	// GET: Groups/Edit/5
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null) return NotFound();

		var @group = await _context
		                  .Groups
		                  .AsNoTracking()
		                  .Include(group => group.Supervisor)
		                  .Include(group => group.DidacticCycle)
		                  .Include(group => group.Students)
		                  .FindAsync(id);
		if (@group == null) return NotFound();

		// Pass context data to the view

		var editGroup = new EditGroup(
			group.Id,
			group.Name,
			group.Supervisor.Id,
			group.Students.Select(student => student.Id).ToList(),
			group.DidacticLevel,
			group.DidacticCycle.Id);
		await SetContext();
		ViewBag.Students.AddRange(_context
		                         .Students
		                         .AsNoTracking()
		                         .Where(student => student.Group != null &&
		                                           student
			                                          .Group
			                                          .Id == group.Id));
		return View(editGroup);
	}

	// POST: Groups/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, EditGroup @group)
	{
		if (id != @group.Id) return NotFound();

		var supervisor = await _context
		                      .Teachers
		                      .FindAsync(@group.SupervisorId);
		var didacticCycle = await _context
		                         .DidacticCycles
		                         .FindAsync(@group.DidacticCycleId);

		var students = @group
		              .StudentIds
		             ?.Select(async id => await
			               _context
				              .Students
				              .FindAsync(id))
		              .Select(task => task.Result)
		              .ToList();
		if (supervisor is null)
			ModelState.AddModelError(nameof(@group.SupervisorId), "Supervisor must be a valid teacher");
		if (didacticCycle is null)
			ModelState.AddModelError(nameof(@group.DidacticCycleId), "Didactic cycle must be a valid period");

		if (students?.Any(student => student == null) ?? false)
			ModelState.AddModelError(nameof(@group.StudentIds), "All members must be active students");

		if (students.Any(student => student != null && student.Group != null))
			ModelState.AddModelError(nameof(@group.StudentIds), "All students must be unassigned to other groups");

		if (!ModelState.IsValid)
		{
			await SetContext();
			return View(group);
		}

		try
		{
			var newGroup = await _context
			                    .Groups
			                    .Include(group => group.Supervisor)
			                    .Include(group => group.DidacticCycle)
			                    .Include(group => group.Students)
			                    .FindAsync(group.Id);

			newGroup.DidacticCycle = didacticCycle;
			newGroup.DidacticLevel = group.DidacticLevel;
			newGroup.Supervisor    = supervisor;
			newGroup.Students      = students;

			supervisor.Groups.Add(newGroup);
			students.ForEach(student => student.Group = newGroup);
			didacticCycle.Groups.Add(newGroup);

			_context.Update(newGroup);
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateException dbUpdateException) when (dbUpdateException.InnerException is SqlException sqlException
		                                               && sqlException.Number == 2601)
		{
			ModelState.AddModelError("Name", "Group with this name already exists.");
			await SetContext();
			return View(group);
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!GroupExists(group.Id)) return NotFound();

			throw;
		}
		catch (KeyNotFoundException)
		{
			return NotFound();
		}

		return RedirectToAction(nameof(Index));
	}

	// GET: Groups/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null) return NotFound();

		var @group = await _context
		                  .Groups
		                  .FindAsync(id);
		if (@group == null) return NotFound();

		return View(@group);
	}

	// POST: Groups/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var @group = await _context.Groups.FindAsync(id);
		if (@group != null) _context.Groups.Remove(group);

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool GroupExists(Guid id)
	{
		return _context.Groups.Any(e => e.Id == id);
	}
}