using Dziennik.Models;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Linq;

public static class Linq
{
	public static T? Find<T>(this IEnumerable<T?> queryable, Guid? id)
		where T : Entity
	{
		return queryable.FirstOrDefault(entity => entity != null && entity.Id == id);
	}

	public static async Task<T?> FindAsync<T>(this IQueryable<T?> queryable, Guid? id)
		where T : Entity
	{
		return await queryable.FirstOrDefaultAsync(entity => entity != null && entity.Id == id);
	}

	public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> tasks)
	{
		return await Task.WhenAll(tasks);
	}

	public static async Task<List<T>> ListOfAwaited<T>(this IEnumerable<Task<T>> tasks)
	{
		return (await tasks.WhenAll()).ToList();
	}
}