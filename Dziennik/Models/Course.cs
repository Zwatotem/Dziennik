using Dziennik.Models.Roles;
using Dziennik.Models.Scheduling;
using Dziennik.Models.Scoring;

namespace Dziennik.Models;

public class Course : Entity
{
	public CourseProgram? Program { get; set; } = null!;
	public Teacher?       Teacher { get; set; }
	public Group?         Group   { get; set; }

	public RecurringPlan? RecurringPlan { get; set; }
	public DetailedPlan?  DetailedPlan  { get; set; }

	#region EFCore

	public required Guid            Id              { get; init; } = Guid.NewGuid();
	public required Guid            ProgramId       { get; set; }
	public          Guid?           TeacherId       { get; set; }
	public          Guid?           GroupId         { get; set; }
	public          Guid?           DidacticCycleId { get; set; }
	public          Group?          Group__         => Group;
	public          List<Event>?    Lessons         { get; set; } = new();
	public          List<ClassTask> Tasks           { get; set; } = new();

	#endregion
}