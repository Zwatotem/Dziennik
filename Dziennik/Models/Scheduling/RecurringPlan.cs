using Dziennik.Models.Scheduling;

namespace Dziennik.Models;

public class RecurringPlan : Entity
{
	public Guid                 Id     { get; init; } = Guid.NewGuid();
	public List<RecurringEvent> Events { get; set; }  = new();
}