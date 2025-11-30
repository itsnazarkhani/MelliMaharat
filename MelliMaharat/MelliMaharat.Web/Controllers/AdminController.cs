using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Models.Owned;
using MelliMaharat.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Web.Controllers
{
    [AuthorizeByRole(UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Lessons()
        {
            var lessons = await _unitOfWork.Lessons
                .GetAll()
                .OrderBy(l => l.Name)
                .ToListAsync();

            return View(lessons);
        }

        public IActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            if (!ModelState.IsValid)
                return View(lesson);

            await _unitOfWork.Lessons.AddAsync(lesson);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Lessons");
        }

        public async Task<IActionResult> Masters()
        {
            var masters = await _unitOfWork.Masters
                .GetAll()
                .Include(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(m => m.Department)
                .OrderBy(m => m.User.PersonInformation.LastName)
                .ToListAsync();

            return View(masters);
        }

        public async Task<IActionResult> AddMaster()
        {
            ViewBag.Departments = await _unitOfWork.Departments.GetAll().ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMaster(Master master, Person person)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _unitOfWork.Departments.GetAll().ToListAsync();
                return View(master);
            }

            // Create User
            var user = new User
            {
                PersonInformation = person,
                Username = person.NationalCode,
                Password = person.PhoneNumber,
                Role = UserRoles.Master
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitChangesAsync(); // Save user first to get Id

            // Assign UserId to Master
            master.UserId = user.Id;

            await _unitOfWork.Masters.AddAsync(master);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Masters");
        }

        public IActionResult Students() => View();
        public IActionResult Presentations() => View();
        public IActionResult CreateEvent() => View();
    }
}
