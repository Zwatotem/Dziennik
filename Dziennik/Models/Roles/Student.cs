using Dziennik.Models.Scheduling;
using Dziennik.Models.Scoring;

namespace Dziennik.Models.Roles;

// Members that are required by EF Core

public class Student(User owner) : Role("Student", owner)
{
	public          Group? Group { get; set; }
	public override string Name  => "Student";

	public List<PresenceRecord>? PresenceRecords { get; set; }
	public List<Mark>            Marks           { get; set; } = new();

	public override List<Course> RelevantCourses(List<Course> allCourses)
	{
		return allCourses.Where(course => course.Group == Group).ToList();
	}

	public override List<Event> RelevantEvents(List<Event> allEvents)
	{
		return allEvents.Where(@event => @event.Course.Group.Id == Group.Id).ToList();
	}

	#region EFCore

	/// <summary>
	/// Parameterless constructor for EF Core
	/// </summary>
	/// <remarks>Do not use this constructor</remarks>
	[Obsolete]
	public Student() : this((User)null!) {}

	#endregion
}