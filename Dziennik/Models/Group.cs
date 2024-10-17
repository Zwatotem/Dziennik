using Dziennik.Models.Roles;

namespace Dziennik.Models;

public class Group : Entity
{
	public required Guid          Id            { get; init; } = Guid.NewGuid();
	public required string        Name          { get; set; }
	public          Guid?         SupervisorId  { get; set; }
	public          Teacher?      Supervisor    { get; set; }
	public          List<Student> Students      { get; set; } = new();
	public required DidacticLevel DidacticLevel { get; set; }
	public required DidacticCycle DidacticCycle { get; set; }
	public          List<Course>  Courses       { get; init; } = new();
}

public record CreateGroup(
	string        Name,
	Guid?         SupervisorId,
	List<Guid>    StudentIds,
	DidacticLevel DidacticLevel,
	Guid          DidacticCycleId);

public record EditGroup(
	Guid          Id,
	string        Name,
	Guid?         SupervisorId,
	List<Guid>?   studentIds,
	DidacticLevel DidacticLevel,
	Guid          DidacticCycleId)
{
	public List<Guid> StudentIds { get; set; } = studentIds ?? [];
}