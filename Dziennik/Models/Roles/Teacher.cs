using Dziennik.Models.Scheduling;
using Dziennik.Models.Scoring;

namespace Dziennik.Models.Roles;

public class Teacher(User owner) : Role("Teacher", owner)
{
	public          List<Group>         Groups         { get; init; } = new();
	public          List<Course>        Courses        { get; init; } = new();
	public          List<CourseProgram> CoursePrograms { get; init; } = new();
	public override string              Name           => "Teacher";
	public          List<ClassTask>     Tasks          { get; set; }
	public          List<Mark>          Marks          { get; set; }

	public override List<Course> RelevantCourses(List<Course> allCourses)
	{
		return allCourses.Where(course => course.Teacher == this).ToList();
	}

	public override List<Event> RelevantEvents(List<Event> allEvents)
	{
		return allEvents.Where(e => e.Course.Teacher.Id == Id).ToList();
	}

	#region EFCore

	/// <summary>
	/// Parameterless constructor for EF Core
	/// </summary>
	/// <remarks>Do not use this constructor</remarks>
	[Obsolete]
	public Teacher() : this((User)null!) {}

	#endregion
}