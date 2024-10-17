using Dziennik.Data;
using Dziennik.Linq;
using Dziennik.Models;
using Dziennik.Models.Roles;
using Dziennik.Models.Scoring;
using Dziennik.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeControlller = Dziennik.Controllers.HomeController;

namespace Dziennik.Controllers
{
	public class Marks(ApplicationDbContext context, UserManager<User> userManager) : Controller
	{
		private readonly ApplicationDbContext _context     = context;
		private readonly UserManager<User>    _userManager = userManager;

		public async Task<ActionResult> Index(Guid taskId)
		{
			var user = await _context
			                .Users
			                .FindAsync(User);
			user = await _context
			            .Users
			            .Include(user => user.Roles)
			            .FindAsync(user.Id);
			if (user.Roles.OfType<Student>().Any())
				return RedirectToAction(nameof(ByStudent));
			return RedirectToAction(nameof(ByTask), new { taskId });
		}

		// GET: Marks
		[Authorize(Roles = "Teacher, Admin")]
		public ActionResult ByTask(Guid taskId)
		{
			var marks = _context
			           .Marks
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.Course)
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.TaskMaster)
			           .Include(mark => mark.Reciever)
			           .Where(m => m.TaskId == taskId);
			ViewBag.TaskId   = taskId;
			ViewBag.TaskName = _context.Tasks.AsNoTracking().Find(taskId).Description;
			return View(marks);
		}

		[Authorize(Roles = "Student, Parent")]
		public ActionResult ByStudent(Guid studentId)
		{
			var marks = _context
			           .Marks
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.Course)
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.TaskMaster)
			           .Include(mark => mark.Reciever)
			           .Where(m => m.RecieverId == studentId);
			ViewBag.StudentId   = studentId;
			ViewBag.StudentName = _context.Students.AsNoTracking().Find(studentId).Owner.FullName;
			return View(marks);
		}

		[Authorize(Roles = "Student, Parent")]
		public async Task<ActionResult> ByStudentCourse(Guid studentId, Guid courseId)
		{
			if (studentId == Guid.Empty)
			{
				var user = await _userManager.GetUserAsync(User);
				user = _context.Users
				              .Include(user => user.Roles)
				              .Find(user.Id);
				var student = user.Roles.OfType<Student>().FirstOrDefault();
				studentId = student.Id;
			}
			var marks = _context
			           .Marks
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.Course)
			           .Include(mark => mark.ClassTask)
			           .ThenInclude(task => task.TaskMaster)
			           .Include(mark => mark.Reciever)
			           .Where(m => m.RecieverId == studentId && m.ClassTask.CourseId == courseId);
			ViewBag.StudentId  = studentId;
			ViewBag.CourseId   = courseId;
			ViewBag.CourseName = _context.Courses.AsNoTracking().Find(courseId).Program.Description;
			return View(marks);
		}

		// GET: Marks/Details/5
		// public ActionResult Details(Guid id)
		// {
		//     return View();
		// }

		// GET: Marks/Create
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<ActionResult> Create(Guid taskId)
		{
			var task = await _context.Tasks
			                         .Include(task => task.Course)
			                         .ThenInclude(course => course.Program)
			                         .Include(task => task.Course)
			                         .ThenInclude(course => course.Group)
			                         .ThenInclude(group => group.Students)
			                         .Include(task => task.TaskMaster)
			                         .FindAsync(taskId);
			var students = task.Course.Group.Students;
			ViewBag.TaskId   = taskId;
			ViewBag.Students = students;
			return View();
		}

		// POST: Marks/Create
		[Authorize(Roles = "Teacher, Admin")]
		[HttpPost]
		public async Task<ActionResult> Create(Mark mark)
		{
			mark.Date      = DateTime.Now;
			mark.ClassTask = await _context.Tasks.FindAsync(mark.TaskId);
			mark.Reciever  = await _context.Students.FindAsync(mark.RecieverId);
			mark.ClassTask.Marks.Add(mark);
			mark.Reciever.Marks.Add(mark);
			await _context.Marks.AddAsync(mark);
			_context.Update(mark.ClassTask);
			_context.Update(mark.Reciever);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(ByTask), new { taskId = mark.TaskId });
		}

		// GET: Marks/Edit/5
		[Authorize(Roles = "Teacher, Admin")]
		public async Task<ActionResult> Edit(Guid taskId, Guid id)
		{
			var task = await _context.Tasks
			                         .Include(task => task.Course)
			                         .ThenInclude(course => course.Program)
			                         .Include(task => task.Course)
			                         .ThenInclude(course => course.Group)
			                         .ThenInclude(group => group.Students)
			                         .Include(task => task.TaskMaster)
			                         .FindAsync(taskId);
			var students = task.Course.Group.Students;
			var mark     = _context.Marks.Find(id);
			ViewBag.TaskId   = taskId;
			ViewBag.Students = students;
			return View(mark);
		}

		// POST: Marks/Edit/5
		[Authorize(Roles = "Teacher, Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, Mark mark)
		{
			
			try
			{
				return RedirectToAction(nameof(ByTask));
			}
			catch
			{
				return View();
			}
		}

		// GET: Marks/Delete/5
		[Authorize(Roles = "Teacher, Admin")]
		public ActionResult Delete(Guid id)
		{
			return View();
		}

		// POST: Marks/Delete/5
		[Authorize(Roles = "Teacher, Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(ByTask));
			}
			catch
			{
				return View();
			}
		}
	}
}