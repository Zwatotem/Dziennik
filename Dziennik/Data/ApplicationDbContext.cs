using Dziennik.Models;
using Dziennik.Models.Roles;
using Dziennik.Models.Scheduling;
using Dziennik.Models.Scoring;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dziennik.Data;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
	public DbSet<Role>          IndividualRoles { get; set; }
	public IQueryable<Teacher?> Teachers        => IndividualRoles.OfType<Teacher>();
	public IQueryable<Student>  Students        => IndividualRoles.OfType<Student>();
	public IQueryable<Parent>   Parents         => IndividualRoles.OfType<Parent>();
	public DbSet<RoleRequest>   RoleRequests    { get; set; }
	public DbSet<Group>         Groups          { get; set; }
	public DbSet<DidacticCycle> DidacticCycles  { get; set; }
	public DbSet<CourseProgram> CoursePrograms  { get; set; }
	public DbSet<Course>        Courses         { get; set; }

	public IQueryable<Course> CoursesWithDependencies =>
		Courses
		 .Include(c => c.Program)
		 .Include(c => c.Teacher)
		 .Include(c => c.RecurringPlan)
		 .Include(c => c.DetailedPlan)
		 .Include(c => c.Group)
		 .ThenInclude(group => group.DidacticCycle)
		 .Include(course => course.Tasks)
		 .ThenInclude(task => task.Marks)
		 .ThenInclude(mark => mark.Reciever);

	public DbSet<RecurringPlan>  RecurringPlans  { get; set; }
	public DbSet<DetailedPlan>   DetailedPlans   { get; set; }
	public DbSet<RecurringEvent> RecurringEvents { get; set; }
	public DbSet<Event>          Events          { get; set; }
	public IQueryable<Event>     Lessons         => Events.OfType<Event>();

	public DbSet<PresenceLog>    PresenceLogs    { get; set; }
	public DbSet<PresenceRecord> PresenceRecords { get; set; }
	public DbSet<ClassTask>      Tasks           { get; set; }
	public DbSet<Mark>           Marks           { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	protected override void OnModelCreating(ModelBuilder builder)
	{
		var makeRole = (string id, string name) => new IdentityRole<Guid>(name)
			{ Id = Guid.Parse(id), NormalizedName = name.ToUpperInvariant() };

		builder.Entity<IdentityRole<Guid>>()
		       .HasData([
			        makeRole("acbf4c1d-78c4-4e39-ad6c-faf4483e86d3", "Admin"),
			        makeRole("554c8032-faed-41d5-acde-1ac0f137b91a", "Teacher"),
			        makeRole("0423dd4b-d53f-4ca0-8728-91ea982244b5", "Student"),
			        makeRole("c1c73fcf-07fe-4df0-96e3-7272acfda5df", "Parent")
		        ]);
		// Role
		builder.Entity<Role>()
		       .HasOne<User>(role => role.Owner)
		       .WithMany(user => user.Roles)
		       .OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Role>()
		       .Navigation(r => r.Owner)
		       .AutoInclude();

		builder.Entity<Admin>().HasBaseType<Role>();

		// RoleRequest
		builder.Entity<RoleRequest>()
		       .HasOne<User>(roleRequest => roleRequest.User)
		       .WithMany(user => user.RoleRequests)
		       .OnDelete(DeleteBehavior.NoAction);

		// Group
		builder.Entity<Group>(group =>
		{
			group
			 .HasOne<Teacher>(group => group.Supervisor)
			 .WithMany(teacher => teacher.Groups)
			 .HasForeignKey(group => group.SupervisorId)
			 .OnDelete(DeleteBehavior.NoAction);

			group
			 .HasMany<Student>(group => group.Students)
			 .WithOne(student => student.Group)
			 .OnDelete(DeleteBehavior.NoAction);

			group
			 .HasOne<DidacticCycle>(group => group.DidacticCycle)
			 .WithMany(didacticCycle => didacticCycle.Groups)
			 .OnDelete(DeleteBehavior.NoAction);

			group
			 .HasIndex(group => group.Name)
			 .IsUnique();
		});

		// Course Program
		builder.Entity<CourseProgram>()
		       .HasMany<Teacher>(courseProgram => courseProgram.DedicatedTeachers)
		       .WithMany(teacher => teacher.CoursePrograms);

		// Course
		builder.Entity<Course>(course =>
		{
			course
			 .HasOne<CourseProgram>(course => course.Program)
			 .WithMany(program => program.Courses)
			 .HasForeignKey(course => course.ProgramId)
			 .OnDelete(DeleteBehavior.Cascade);

			course
			 .HasOne<Group>(course => course.Group)
			 .WithMany(group => group.Courses)
			 .HasForeignKey(course => course.GroupId)
			 .OnDelete(DeleteBehavior.NoAction);

			course
			 .HasOne<Teacher>(course => course.Teacher)
			 .WithMany(teacher => teacher.Courses)
			 .HasForeignKey(course => course.TeacherId)
			 .OnDelete(DeleteBehavior.NoAction);

			course
			 .HasMany<Event>(course => course.Lessons)
			 .WithOne(lesson => lesson.Course)
			 .OnDelete(DeleteBehavior.Cascade);

			course
			 .HasOne<RecurringPlan>(course => course.RecurringPlan);

			course
			 .HasOne<DetailedPlan>(course => course.DetailedPlan);
		});

		// RecurringPlan
		builder.Entity<RecurringPlan>()
		       .HasMany<RecurringEvent>(plan => plan.Events);

		builder.Entity<RecurringPlan>()
		       .Navigation(plan => plan.Events)
		       .AutoInclude();

		// DetailedPlan
		builder.Entity<DetailedPlan>()
		       .HasMany<Event>(plan => plan.Events);

		builder.Entity<DetailedPlan>()
		       .Navigation(plan => plan.Events)
		       .AutoInclude();

		// RecuringEvent
		builder.Entity<RecurringEvent>()
		       .HasMany<Event>(recurring => recurring.Events)
		       .WithOne(@event => @event.Template)
		       .OnDelete(DeleteBehavior.NoAction);

		// Event
		builder.Entity<Event>(lesson =>
		{
			lesson.HasOne<Course>(lesson => lesson.Course)
			      .WithMany(course => course.Lessons);
		});

		// Presence
		builder.Entity<PresenceLog>()
		       .HasMany<PresenceRecord>(log => log.Students)
		       .WithOne(record => record.PresenceLog);

		builder.Entity<PresenceRecord>(record =>
			{
				record.HasIndex(record => record.StudentId);
				record.HasKey(record => new { record.StudentId, record.PresenceLogId });
				record.HasOne<Student>(record => record.Student)
				      .WithMany(student => student.PresenceRecords);
			}
		);

		// Tasks

		builder.Entity<ClassTask>(task =>
		{
			task.HasOne<Teacher>(task => task.TaskMaster)
			    .WithMany(teacher => teacher.Tasks)
			    .OnDelete(DeleteBehavior.NoAction)
			    .HasForeignKey(task => task.TaskMasterId);
			task.HasOne<Course>(task => task.Course)
			    .WithMany(course => course.Tasks)
			    .OnDelete(DeleteBehavior.NoAction)
			    .HasForeignKey(task => task.CourseId);
		});

		builder.Entity<Mark>(mark =>
		{
			mark.HasOne<Student>(mark => mark.Reciever)
			    .WithMany(student => student.Marks)
			    .OnDelete(DeleteBehavior.NoAction)
			    .HasForeignKey(mark => mark.RecieverId);
			mark.HasOne<ClassTask>(mark => mark.ClassTask)
			    .WithMany(task => task.Marks)
			    .OnDelete(DeleteBehavior.NoAction)
			    .HasForeignKey(mark => mark.TaskId);
		});
		base.OnModelCreating(builder);
	}
}