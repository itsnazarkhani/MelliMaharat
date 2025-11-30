using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
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




        public IActionResult Masters() => View();
        public IActionResult Students() => View();
        public IActionResult Presentations() => View();
        public IActionResult CreateEvent() => View();
    }
}
