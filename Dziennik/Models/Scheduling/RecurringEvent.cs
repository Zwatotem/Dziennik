using Dziennik.Data;
using Microsoft.Extensions.Internal;

namespace Dziennik.Models.Scheduling;

public class RecurringEvent : Entity
{
	public Guid        Id         { get; init; } = Guid.NewGuid();
	public DayOfWeek   DayOfWeek  { get; set; }
	public TimeOnly    StartTime  { get; set; }
	public TimeOnly    EndTime    { get; set; }
	public List<Event> Events     { get; set; } = new();
	public int         RoomNumber { get; set; }

	public async Task UpdateHours(
		TimeOnly             eventStartTime,
		TimeOnly             eventEndTime,
		ApplicationDbContext context,
		ISystemClock         clock)
	{
		StartTime = eventStartTime;
		EndTime   = eventEndTime;
		foreach (var @event in Events.Where(@event => @event.StartDate > clock.UtcNow.DateTime))
		{
			@event.StartDate = @event.StartDate.Date + StartTime.ToTimeSpan();
			@event.EndDate   = @event.EndDate__.Date + EndTime.ToTimeSpan();
		}

		await context.SaveChangesAsync();
	}

	public async Task ClearEvents(ApplicationDbContext context, ISystemClock clock)
	{
		Events = Events.Where(@event => @event.EndDate < clock.UtcNow.DateTime).ToList();
		foreach (var @event in Events.Where(@event => @event.StartDate > clock.UtcNow.DateTime))
		{
			context.Events.Remove(@event);
		}

		await context.SaveChangesAsync();
	}

	public void Update(RecurringEvent @event)
	{
		DayOfWeek = @event.DayOfWeek;
		StartTime = @event.StartTime;
		EndTime   = @event.EndTime;
	}
}

public record CreateRecurringEvent(
	Guid           ScheduleId,
	RecurringEvent Event
);

public record EditRecurringEvent(
	Guid           ScheduleId,
	RecurringEvent Event
);

public record DeleteRecurringEvent(
	Guid scheduleId,
	Guid eventId
)
{
	public Guid ScheduleId { get; init; } = scheduleId;
	public Guid EventId    { get; init; } = eventId;
}