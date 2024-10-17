using Dziennik.Models.Roles;

namespace Dziennik.Models.Scoring;

public class Mark : Entity
{
	public Teacher   Issuer    => ClassTask?.Course?.Teacher;
	public Student   Reciever  { get; set; }
	public decimal   Value     { get; set; }
	public ClassTask ClassTask { get; set; }
	public DateTime  Date      { get; set; }

	#region EFCore

	public Guid Id         { get; init; } = Guid.NewGuid();
	public Guid RecieverId { get; set; }
	public Guid TaskId     { get; set; }

	#endregion
}