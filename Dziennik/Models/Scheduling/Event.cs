namespace Dziennik.Models.Scheduling;

public class Event : Entity
{
	public          Guid            Id          { get; init; } = Guid.NewGuid();
	public          DateTime        StartDate   { get; set; }
	public          DateTime        EndDate     { get; set; }
	public required string          Description { get; set; }
	public          RecurringEvent? Template    { get; set; }
	public required Course?         Course      { get; set; }
	public          int?            RoomNumber  { get; set; }
	public          PresenceLog?    Presence    { get; set; }

	public DateTime EndDate__ => EndDate;
}