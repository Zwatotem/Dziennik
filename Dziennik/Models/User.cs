using Dziennik.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace Dziennik.Models;

public class User : IdentityUser<Guid>, Entity
{
	public          List<string>      Names        { get; set; } = new();
	public          string            FirstName    => Names[0];
	public required string            Surname      { get; set; }
	public required string            PESEL        { get; set; }
	public          List<Role>        Roles        { get; init; } = new();
	public          List<RoleRequest> RoleRequests { get; init; } = new();

	public string ShortName => $"{FirstName} {Surname}";
	public string FullName  => $"{string.Join(" ", Names)} {Surname}";

	public async Task AddRole(Role role, UserManager<User> userManager)
	{
		Roles.Add(role);
		role.Owner = this;
		await userManager.AddToRoleAsync(this, role.Name);
	}
}

public class DTUser
{
	public string?           Names        { get; set; }
	public string?           Surname      { get; set; }
	public string?           PESEL        { get; set; }
	public List<Role>        Roles        { get; set; } = new();
	public List<RoleRequest> RoleRequests { get; set; } = new();
	public string?           PhoneNumber  { get; set; }
	public string?           Email        { get; set; }
	public string?           UserName     { get; set; }
}