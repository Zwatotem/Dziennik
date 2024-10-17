namespace Dziennik.Models;

public class DidacticCycle(DateTime start, DateTime end) : Entity
{
	public Guid         Id            { get; set; } = Guid.NewGuid();
	public DateTime     StartDate     { get; set; } = start;
	public DateTime     EndDate       { get; set; } = end;
	public string       PrettyDisplay => $"{StartDate.Year}/{EndDate.Year}";
	public List<Group>  Groups        { get; set; } = new();
	public List<Course> Courses       { get; set; } = new();

	public bool Contains(DateTime date)
	{
		return StartDate <= date && date <= EndDate;
	}

	#region EFCore

	/// <summary>
	///  This constructor is only for Entity Framework Core.
	/// </summary>
	/// <remarks>
	/// Do not use this constructor in your code.
	/// </remarks>
	[Obsolete]
	public DidacticCycle() : this((DateTime)default, (DateTime)default) {}

	#endregion
}