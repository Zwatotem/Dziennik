using Dziennik.Data;
using Dziennik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace Dziennik.Services;

public class DidacticCycleAdvancer
{
	public static    IServiceProvider?    serviceProvider;
	private readonly ApplicationDbContext context;
	private readonly ISystemClock         clock;

	public DidacticCycleAdvancer(ApplicationDbContext? context, ISystemClock? clock)
	{
		if (serviceProvider is null
		 && (context is null || clock is null))
			throw new ArgumentNullException(nameof(serviceProvider));
		this.context = context ?? serviceProvider!.GetRequiredService<ApplicationDbContext>();
		this.clock   = clock ?? serviceProvider!.GetRequiredService<ISystemClock>();
	}

	public async Task Advance(CancellationToken stoppingToken = default)
	{
		var now = clock.UtcNow.DateTime;
		await PopulateCurrent(now, stoppingToken);
		await PopulateIncoming(now, stoppingToken);
	}

	private async Task PopulateIncoming(DateTime now, CancellationToken stoppingToken)
	{
		var latest = await context.DidacticCycles
		                          .OrderByDescending(didacticCycle => didacticCycle.StartDate)
		                          .FirstOrDefaultAsync(stoppingToken);
		if (latest is not null
		 && now + TimeSpan.FromDays(365) < latest.EndDate) return;

		var      start    = latest?.EndDate ?? new DateTime(now.Year, 09, 01);
		DateTime end      = new(start.Year + 1, 09, 01);
		var      newCycle = new DidacticCycle(start, end);

		await context.DidacticCycles.AddAsync(newCycle, stoppingToken);
		await context.SaveChangesAsync(stoppingToken);
	}

	private async Task PopulateCurrent(DateTime now, CancellationToken stoppingToken)
	{
		var current = await context.DidacticCycles
		                           .FirstOrDefaultAsync(cycle => cycle.StartDate <= now
		                                                      && now <= cycle.EndDate,
			                            stoppingToken);

		if (current is not null) return;

		var candidate1 = new DidacticCycle(
			new DateTime(now.Year,     09, 01),
			new DateTime(now.Year + 1, 09, 01));

		var candidate2 = new DidacticCycle(
			new DateTime(now.Year - 1, 09, 01),
			new DateTime(now.Year,     09, 01));

		var newCycle = candidate1.Contains(now) ? candidate1 : candidate2;

		await context.DidacticCycles.AddAsync(newCycle, stoppingToken);
		await context.SaveChangesAsync(stoppingToken);
	}
}