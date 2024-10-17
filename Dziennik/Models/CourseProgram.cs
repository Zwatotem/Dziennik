using Dziennik.Models.Roles;

namespace Dziennik.Models;

public class CourseProgram : Entity
{
	public Guid          Id                { get; init; } = Guid.NewGuid();
	public string?       Description       { get; set; }
	public DidacticLevel DidacticLevel     { get; set; }
	public List<Teacher> DedicatedTeachers { get; set; }  = new();
	public List<Course>  Courses           { get; init; } = new();
}

public record CreateCourseProgram(
	string?       Description,
	DidacticLevel DidacticLevel,
	List<Guid>    DedicatedTeachers);

public record EditCourseProgram(
	Guid          Id,
	string?       Description,
	DidacticLevel DidacticLevel,
	List<Guid>?   DedicatedTeachers);