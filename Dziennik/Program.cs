using Dziennik.Areas.Identity;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Middlewares;
using Dziennik.Models;
using Dziennik.Services;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Internal;

//////////////////// Builder /////////////////////////

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<PhoneNumberTokenProvider<User>>();
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
       .AddDefaultUI()
       .AddTokenProvider<TwoFactorTokenProvider<User>>("Default")
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddHangfire(config => { config.UseSqlServerStorage(connectionString); });
// Add DidacticCycleAdvancer as a RecurringJob
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddTransient<ISystemClock, SystemClock>();
builder.Services.AddTransient<DidacticCycleAdvancer>();
builder.Services.AddTransient<LessonAdvancer>();
builder.Services.AddHangfireServer();

builder.Services.AddRazorPages();

#pragma warning disable ASP0000
var serviceProvider = builder.Services.BuildServiceProvider();
DidacticCycleAdvancer.serviceProvider = serviceProvider;
LessonAdvancer.serviceProvider        = serviceProvider;
#pragma warning restore ASP0000

var app = builder.Build();

//////////////////// App /////////////////////////

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<DidacticCycleAdvancer>("DidacticCycleAdvancer",
	advancer => advancer.Advance(CancellationToken.None), Cron.Daily);
RecurringJob.AddOrUpdate<LessonAdvancer>("LessonAdvancer",
	advancer=> advancer.Advance(CancellationToken.None), Cron.Hourly);


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// throw new NotImplementedException("""
//  TODO: Then schedules
//  """);

app.Run();