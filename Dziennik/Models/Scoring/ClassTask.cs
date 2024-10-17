using Dziennik.Models.Roles;

namespace Dziennik.Models.Scoring;

public class ClassTask : Entity
{
	public Teacher    TaskMaster  { get; set; }
	public string     Description { get; set; }
	public Course     Course      { get; set; }
	public decimal    Weight      { get; set; }
	public List<Mark> Marks       { get; set; } = new();

	#region EFCore

	public Guid Id           { get; init; } = Guid.NewGuid();
	public Guid TaskMasterId { get; set; }
	public Guid CourseId     { get; set; }

	#endregion
}