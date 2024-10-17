using Dziennik.Models.Roles;

namespace Dziennik.Models;

public class RoleRequest : Entity
{
	public          Guid Id            { get; set; } = Guid.NewGuid();
	public required User User          { get; set; }
	public required Role RequestedRole { get; set; }

	public enum RequestStatus
	{
		Pending,
		Rejected,
		Approved
	}

	public required RequestStatus Status { get; set; }


	public required DateTime  RequestDate  { get; init; } = DateTime.Now;
	public          DateTime? DecisionDate { get; set; }
}