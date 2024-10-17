using Dziennik.Models;
using Microsoft.AspNetCore.Identity;

namespace Dziennik.Areas.Identity;

public class TwoFactorTokenProvider<TUser>(PhoneNumberTokenProvider<TUser> phoneNumberTokenProvider)
	: IUserTwoFactorTokenProvider<TUser>
	where TUser : User
{
	public async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
	{
		return await phoneNumberTokenProvider.GenerateAsync(purpose, manager, user);
	}

	public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
	{
		return await phoneNumberTokenProvider.ValidateAsync(purpose, token, manager, user);
	}

	public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
	{
		return await phoneNumberTokenProvider.CanGenerateTwoFactorTokenAsync(manager, user);
	}
}