using Dziennik.Models.Scheduling;

namespace Dziennik.Models.Roles;

public class Admin(User owner) : Role("Admin", owner)
{
	public override string Name => "Admin";

	public override List<Course> RelevantCourses(List<Course> allCourses)
		=> allCourses.ToList();

	public override List<Event> RelevantEvents(List<Event> allEvents)
		=> allEvents.ToList();

	#region EFCore

	/// <summary>
	/// Parameterless constructor for EF Core
	/// </summary>
	/// <remarks>Do not use this constructor</remarks>
	[Obsolete]
	public Admin() : this(null!) {}

	#endregion
}