using Dziennik.Models.Scheduling;

namespace Dziennik.Models.Roles;

// Members that are required by EF Core

public class Parent(User owner, Student child) : Role("Parent", owner)
{
	public          Student Child { get; set; } = child;
	public override string  Name  => "Parent";

	public override List<Course> RelevantCourses(List<Course> allCourses)
	{
		return Child.RelevantCourses(allCourses);
	}

	public override List<Event> RelevantEvents(List<Event> allEvents)
	{
		return Child.RelevantEvents(allEvents);
	}

	#region EFCore

	/// <summary>
	/// Parameterless constructor for EF Core
	/// </summary>
	/// <remarks>Do not use this constructor</remarks>
	[Obsolete]
	public Parent() : this((User)null!, (Student)null!) {}

	#endregion
}