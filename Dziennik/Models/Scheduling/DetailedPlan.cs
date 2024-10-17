namespace Dziennik.Models.Scheduling;

public class DetailedPlan : Entity
{
	public Guid        Id     { get; init; } = Guid.NewGuid();
	public List<Event> Events { get; init; } = new();
}