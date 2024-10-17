// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dziennik.Views.UserManagement;

/// <summary>
///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
///     directly from your code. This API may change or be removed in future releases.
/// </summary>
public static class ManageNavPages
{
	public static string Index(ViewContext viewContext)
	{
		return PageNavClass(viewContext, "Index");
	}

	public static string RoleRequests(ViewContext viewContext)
	{
		return PageNavClass(viewContext, "Role Requests");
	}

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public static string PageNavClass(ViewContext viewContext, string page)
	{
		var activePage = viewContext.ViewData["ActivePage"] as string
		              ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
		return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
	}
}