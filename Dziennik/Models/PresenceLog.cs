using Dziennik.Models.Roles;

namespace Dziennik.Models;

public class PresenceLog : Entity
{
	public enum PresenceType
	{
		Present,
		Excused,
		Unexcused,
		Justified
	}

	public List<PresenceRecord> Students { get; set; } = new();
	public PresenceType         Teacher  { get; set; }

	#region EFCore

	public Guid Id        { get; init; } = Guid.NewGuid();
	public Guid TeacherId { get; set; }

	#endregion
}

public class PresenceRecord
{
	public          Student                  Student     { get; set; }
	public required PresenceLog              PresenceLog { get; set; }
	public required PresenceLog.PresenceType Presence    { get; set; }

	#region EFCore

	public Guid StudentId     { get; set; }
	public Guid PresenceLogId { get; set; }

	#endregion
}