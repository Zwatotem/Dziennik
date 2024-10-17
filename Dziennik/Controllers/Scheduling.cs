using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models.Scheduling;
using Dziennik.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace Dziennik.Controllers;

public class Scheduling(ApplicationDbContext context, LessonAdvancer lessonAdvancer, ISystemClock clock) : Controller
{
	private readonly ApplicationDbContext _context        = context;
	private readonly LessonAdvancer       _lessonAdvancer = lessonAdvancer;
	private readonly ISystemClock         _clock          = clock;

	[HttpGet]
	public async Task<IActionResult> Index(Guid? id)
	{
		var schedule = await _context
		                    .RecurringPlans
		                    .FindAsync(id);
		if (schedule is null) return NotFound("Schedule not found.");
		var events = schedule.Events;
		ViewBag.id = id;
		return View(events);
	}

	[Authorize(Roles = "Admin, Teacher")]
	[HttpGet]
	public IActionResult Create(Guid planId) => View(new CreateRecurringEvent(planId, new()));

	[HttpPost]
	public async Task<IActionResult> Create(CreateRecurringEvent creationForm)
	{
		var schedule = await _context.RecurringPlans.FindAsync(creationForm.ScheduleId);
		var @event   = creationForm.Event;
		if (@event == null!) ModelState.AddModelError(nameof(creationForm.Event), "Event cannot be null.");
		if (@event.StartTime >= @event.EndTime)
			ModelState.AddModelError(nameof(@event.EndTime), "End time must be after start time.");
		if (schedule is null) return NotFound();
		if (!ModelState.IsValid) return View(creationForm);
		_context.RecurringEvents.Add(@event);
		schedule.Events.Add(@event);
		_context.Update(schedule);
		await _context.SaveChangesAsync();
		await _lessonAdvancer.Advance();
		return RedirectToAction(nameof(Index), new { id = schedule.Id });
	}

	[Authorize(Roles = "Admin, Teacher")]
	[HttpGet]
	public async Task<IActionResult> Edit(Guid eventId, Guid planId)
	{
		var @event = await _context.RecurringEvents.FindAsync(eventId);
		if (@event is null) return NotFound();
		return View(new EditRecurringEvent(planId, @event));
	}

	[Authorize(Roles = "Admin, Teacher")]
	[HttpPost]
	public async Task<IActionResult> Edit(EditRecurringEvent editForm)
	{
		// Validate
		var schedule = await _context.RecurringPlans.FindAsync(editForm.ScheduleId);
		var @event   = editForm.Event;
		if (@event == null!) ModelState.AddModelError(nameof(editForm.Event), "Event cannot be null.");
		else if (@event.StartTime >= @event.EndTime)
			ModelState.AddModelError(nameof(@event.EndTime), "End time must be after start time.");
		if (schedule is null) return NotFound();

		// Edit
		if (!ModelState.IsValid) return View(editForm);
		var eventToEdit = await _context
		                       .RecurringEvents
		                       .Include(recurring => recurring.Events)
		                       .FindAsync(@event.Id);
		switch (@event.DayOfWeek == eventToEdit.DayOfWeek,
			@event.StartTime == eventToEdit.StartTime,
			@event.EndTime == eventToEdit.EndTime)
		{
			case (true, true, true):
				return RedirectToAction(nameof(Index), new { id = schedule.Id });
			case (true, _, _):
				eventToEdit.UpdateHours(@event.StartTime, @event.EndTime, _context, _clock);
				break;
			case (false, _, _):
				eventToEdit.Update(@event);
				await eventToEdit.ClearEvents(_context, _clock);
				await _lessonAdvancer.Advance();
				break;
		}

		_context.RecurringEvents.Update(eventToEdit);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index), new { id = schedule.Id });
	}

	[Authorize(Roles = "Admin, Teacher")]
	[HttpGet]
	public async Task<IActionResult> Delete(Guid eventId, Guid planId)

	{
		var @event = await _context.RecurringEvents.FindAsync(eventId);
		var schedules = await context.RecurringPlans
		                             .Where(schedule => schedule.Events.Contains(@event))
		                             .ToListAsync();
		foreach (var schedule in schedules)
		{
			schedule.Events.Remove(@event);
			_context.Update(schedule);
		}

		_context.RecurringEvents.Remove(@event);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index), new { id = planId });
	}
}