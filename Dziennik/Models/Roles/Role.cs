using Dziennik.Models.Scheduling;
using Microsoft.AspNetCore.Identity;

namespace Dziennik.Models.Roles;

public abstract class Role(string name, User owner) : Entity
{
	public         Guid   Id   { get; init; } = Guid.NewGuid();
	public virtual string Name => name;

	public User Owner { get; set; } = owner;
	public static Role FromString(string name, User owner)
	{
#pragma warning disable CS0612 // Type or member is obsolete
		return name switch
		{
			"Admin"   => new Admin(owner),
			"Teacher" => new Teacher(owner),
			"Student" => new Student(owner),
			"Parent"  => new Parent(owner, new Student()),
			_         => throw new ArgumentOutOfRangeException(nameof(name), name, null)
		};
	}
#pragma warning restore CS0612 // Type or member is obsolete
	public async Task AddToUser(User user, UserManager<User> userManager)
	{
		Owner = user;
		user.Roles.Add(this);
		await userManager.AddToRoleAsync(user, Name);
	}

	public abstract List<Course> RelevantCourses(List<Course> allCourses);
	public abstract List<Event>  RelevantEvents(List<Event>   allEvents);

	#region EFCore

	/// <summary>
	/// Parameterless constructor for EF Core
	/// </summary>
	/// <remarks>Do not use this constructor</remarks>
	[Obsolete]
	public Role() : this((string)"Role", (User)null!) {}

	#endregion
}