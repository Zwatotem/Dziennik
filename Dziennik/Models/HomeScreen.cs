using Dziennik.Models.Scheduling;

namespace Dziennik.Models;

public class HomeScreen
{
	public Event       CurrentEvent   { get; set; }
	public List<Event> UpcomingEvents { get; set; }
}