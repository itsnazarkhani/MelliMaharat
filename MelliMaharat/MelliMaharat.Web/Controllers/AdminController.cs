using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Models.Owned;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using static System.Net.Mime.MediaTypeNames;

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

        #region Lessons
        public async Task<IActionResult> Lessons()
        {
            var lessons = await _unitOfWork.Lessons
                .GetAll()
                .OrderBy(l => l.Name)
                .ToListAsync();

            return View(lessons);
        }

        public IActionResult AddLesson() => View();

        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            if (!ModelState.IsValid)
                return View(lesson);

            await _unitOfWork.Lessons.AddAsync(lesson);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Lessons");
        }
        #endregion

        #region Masters
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

            var user = new User
            {
                PersonInformation = person,
                Username = person.NationalCode,
                Password = person.PhoneNumber,
                Role = UserRoles.Master
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitChangesAsync(); // Save user first

            master.UserId = user.Id;
            await _unitOfWork.Masters.AddAsync(master);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Masters");
        }
        #endregion

        #region Students
        public async Task<IActionResult> Students()
        {
            var students = await _unitOfWork.Students
                .GetAll()
                .Include(s => s.User)
                .ThenInclude(u => u.PersonInformation)
                .OrderBy(s => s.User.PersonInformation.LastName)
                .ToListAsync();

            return View(students);
        }

        public IActionResult AddStudent() => View();

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Create User
            var user = new User
            {
                PersonInformation = new Models.Owned.Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = DateOnly.FromDateTime(model.BirthDate),
                    NationalCode = model.NationalCode,
                    PhoneNumber = model.PhoneNumber
                },
                Username = model.NationalCode,
                Password = model.PhoneNumber,
                Role = UserRoles.Student,
                AvatarId = Guid.NewGuid()
            };

            // Handle Avatar File Upload
            if (model.AvatarFile != null && model.AvatarFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
                var ext = Path.GetExtension(model.AvatarFile.FileName).ToLower();

                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("AvatarFile", "فرمت تصویر معتبر نیست.");
                    return View(model);
                }

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/avatars");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, user.AvatarId + ".jpg");

                using var image = SixLabors.ImageSharp.Image.Load(model.AvatarFile.OpenReadStream());
                image.Mutate(x => x.AutoOrient());
                await image.SaveAsync(filePath, new JpegEncoder());
            }

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitChangesAsync();

            var student = new Student { UserId = user.Id };
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Students");
        }

        public async Task<IActionResult> StudentDetails(Guid id)
        {
            // Include User and PersonInformation for the student
            var student = await _unitOfWork.Students
                .GetAll()
                .Include(s => s.User)
                .ThenInclude(u => u.PersonInformation)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            var model = new StudentDetailsViewModel
            {
                FirstName = student.User.PersonInformation.FirstName,
                LastName = student.User.PersonInformation.LastName,
                NationalCode = student.User.PersonInformation.NationalCode,
                BirthDate = student.User.PersonInformation.BirthDate,
                PhoneNumber = student.User.PersonInformation.PhoneNumber,
                AvatarPath = student.User.AvatarId == Guid.Empty
                    ? "/images/default-avatar.jpg"
                    : $"/images/uploads/avatars/{student.User.AvatarId}.jpg"
            };

            return View(model);
        }
        #endregion

        #region Presentations
        public async Task<IActionResult> Presentations()
        {
            var presentations = await _unitOfWork.Presentations
                .GetAll()
                .Include(p => p.Lesson)
                .Include(p => p.Master)
                .ThenInclude(m => m.User)
                .OrderBy(p => p.DayHold)
                .ToListAsync();

            return View(presentations);
        }

        public async Task<IActionResult> AddPresentation()
        {
            ViewBag.Lessons = await _unitOfWork.Lessons.GetAll().OrderBy(l => l.Name).ToListAsync();
            ViewBag.Masters = await _unitOfWork.Masters
                .GetAll()
                .Include(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .OrderBy(m => m.User.PersonInformation.LastName)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPresentation(Presentation presentation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lessons = await _unitOfWork.Lessons.GetAll().OrderBy(l => l.Name).ToListAsync();
                ViewBag.Masters = await _unitOfWork.Masters
                    .GetAll()
                    .Include(m => m.User)
                    .ThenInclude(u => u.PersonInformation)
                    .OrderBy(m => m.User.PersonInformation.LastName)
                    .ToListAsync();

                return View(presentation);
            }

            await _unitOfWork.Presentations.AddAsync(presentation);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("Presentations");
        }
        #endregion

        #region SelectionTime
        public async Task<IActionResult> SelectionEvents()
        {
            var events = await _unitOfWork.SelectionTimes
                .GetAll()
                .OrderBy(e => e.SelectionStart)
                .ToListAsync();

            return View(events);
        }

        public IActionResult AddSelectionEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSelectionEvent(SelectionTime selectionEvent)
        {
            if (!ModelState.IsValid)
                return View(selectionEvent);

            await _unitOfWork.SelectionTimes.AddAsync(selectionEvent);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("SelectionEvents");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelectionEvent(Guid id)
        {
            var selectionEvent = await _unitOfWork.SelectionTimes.GetAsync(id);
            if (selectionEvent == null)
                return NotFound();

            _unitOfWork.SelectionTimes.Delete(selectionEvent);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction("SelectionEvents");
        }

        #endregion
    }

}
