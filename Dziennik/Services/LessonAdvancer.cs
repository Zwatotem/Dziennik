using Dziennik.Data;
using Dziennik.Models;
using Dziennik.Models.Scheduling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace Dziennik.Services;

public class LessonAdvancer
{
	public static    IServiceProvider?    serviceProvider;
	private readonly ApplicationDbContext context;
	private readonly ISystemClock         clock;

	public LessonAdvancer(ApplicationDbContext? context, ISystemClock? clock)
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
		var templates = await context
		                     .RecurringEvents
		                     .Include(template => template.Events)
		                     .ToListAsync();
		var latests = templates
		             .Select(template => template.Events)
		             .Select(events =>
			              events.OrderByDescending(@event => @event.StartDate)
			                    .FirstOrDefault());
		foreach ((var template, var latest) in templates.Zip(latests))
		{
			// Daylight saving time Kappa
			DateTime nextDatePreAddjusted = latest.StartDate;
			DateTime nextDate;
			do
			{
				nextDatePreAddjusted = nextDatePreAddjusted + TimeSpan.FromDays(7);
				nextDate = new DateTime(
					nextDatePreAddjusted.Year, nextDatePreAddjusted.Month, nextDatePreAddjusted.Day,
					template.StartTime.Hour, template.StartTime.Minute, template.StartTime.Second);
				var course = await context.CoursesWithDependencies
				                          .FirstOrDefaultAsync(course => course.RecurringPlan.Events.Contains(template));
				var nextEvent = new Event()
				{
					Description = course switch
					{
						Course _ => $"{course.Program.Description} {course.Group.Name}",
						null     => "",
					},
					StartDate = nextDate,
					EndDate   = nextDate + (template.EndTime - template.StartTime),
					Template  = template,
					Course    = course,
					Presence  = new(),
				};
				await context.Events.AddAsync(nextEvent, stoppingToken);
				course?.DetailedPlan?.Events.Add(nextEvent);
				template.Events.Add(nextEvent);
				context.Update(template);
				await context.PresenceLogs.AddAsync(nextEvent.Presence, stoppingToken);
			} while (nextDate < now + TimeSpan.FromDays(30));
		}

		await context.SaveChangesAsync(stoppingToken);
	}

	private async Task PopulateCurrent(DateTime now, CancellationToken stoppingToken)
	{
		var templates = await context
		                     .RecurringEvents
		                     .Include(template => template.Events)
		                     .ToListAsync();
		var latests = templates
		             .Select(template => template.Events)
		             .Select(events =>
			              events.OrderByDescending(@event => @event.StartDate)
			                    .FirstOrDefault());
		foreach ((var template, var latest) in templates.Zip(latests))
		{
			if (latest is not null) continue;
			var dayDifference                    = template.DayOfWeek - now.DayOfWeek;
			if (dayDifference < 0) dayDifference += 7;
			var day                              = now + TimeSpan.FromDays(dayDifference);
			var nextDate = new DateTime(
				day.Year, day.Month, day.Day,
				template.StartTime.Hour, template.StartTime.Minute, template.StartTime.Second);
			var course = await context.CoursesWithDependencies
			                          .FirstOrDefaultAsync(course => course.RecurringPlan.Events.Contains(template));

			var nextEvent = new Event()
			{
				Description = $"{course?.Program?.Description ?? ""} {course?.Group?.Name ?? ""}",
				StartDate   = nextDate,
				EndDate     = nextDate + (template.EndTime - template.StartTime),
				Template    = template,
				Presence    = new(),
				Course      = course,
				RoomNumber  = template.RoomNumber,
			};
			await context.Events.AddAsync(nextEvent, stoppingToken);
			course?.DetailedPlan?.Events.Add(nextEvent);
			template.Events.Add(nextEvent);
			await context.PresenceLogs.AddAsync(nextEvent.Presence, stoppingToken);
			context.Update(template);
		}

		await context.SaveChangesAsync(stoppingToken);
	}
}