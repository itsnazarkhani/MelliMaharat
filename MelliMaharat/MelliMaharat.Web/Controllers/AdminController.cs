using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Models.Owned;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels.Admin;
using MelliMaharat.Web.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> AddMaster(AddMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _unitOfWork.Departments.GetAll().ToListAsync();
                return View(model);
            }

            // Check if a user with the same NationalCode already exists
            var existingUser = await _unitOfWork.Users
                .GetAll()
                .FirstOrDefaultAsync(u => u.Username == model.NationalCode);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "کاربری با این کد ملی قبلاً ثبت شده است.");
                ViewBag.Departments = await _unitOfWork.Departments.GetAll().ToListAsync();
                return View(model);
            }

            // Create new user
            var user = new User
            {
                PersonInformation = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    NationalCode = model.NationalCode,
                    PhoneNumber = model.PhoneNumber
                },
                Username = model.NationalCode,
                Password = model.PhoneNumber,
                Role = UserRoles.Master
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitChangesAsync(); // Save user first

            var master = new Master
            {
                UserId = user.Id,
                //Graduation = model.Graduation,
                DepartmentId = model.DepartmentId
            };

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

            // Check if a user with the same NationalCode already exists
            var existingUser = await _unitOfWork.Users
                .GetAll()
                .FirstOrDefaultAsync(u => u.Username == model.NationalCode);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "کاربری با این کد ملی قبلاً ثبت شده است.");
                return View(model);
            }

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
            // Load lessons
            var lessons = await _unitOfWork.Lessons
                .GetAll()
                .OrderBy(l => l.Name)
                .ToListAsync();

            // Load masters with related person info
            var masters = await _unitOfWork.Masters
                .GetAll()
                .Include(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .OrderBy(m => m.User.PersonInformation.LastName)
                .ToListAsync();

            // Lessons as SelectList
            ViewBag.Lessons = new SelectList(
                lessons.Select(l => new { l.Id, l.Name }),
                "Id",
                "Name"
            );

            // Masters (flatten full name)
            ViewBag.Masters = new SelectList(
                masters.Select(m => new
                {
                    m.Id,
                    FullName = $"{m.User.PersonInformation.FirstName} {m.User.PersonInformation.LastName}"
                }),
                "Id",
                "FullName"
            );

            // Persian weekdays
            ViewBag.PersianWeekDays = new SelectList(new List<SelectListItem>
            {
                new ("شنبه", "شنبه"),
                new ("یکشنبه", "یکشنبه"),
                new ("دوشنبه", "دوشنبه"),
                new ("سه‌شنبه", "سه‌شنبه"),
                new ("چهارشنبه", "چهارشنبه"),
                new ("پنج‌شنبه", "پنج‌شنبه"),
                new ("جمعه", "جمعه"),
            }, "Value", "Text");

            return View(new Presentation());
        }

        [HttpPost]
        public async Task<IActionResult> AddPresentation(Presentation model)
        {
            // First validate model state
            if (!ModelState.IsValid)
            {
                await ReloadPresentationDropdowns(model);
                return View(model);
            }

            // Validation: Start time < End time
            if (model.StartTime >= model.EndTime)
            {
                ModelState.AddModelError("", "زمان شروع باید قبل از زمان پایان باشد.");
                await ReloadPresentationDropdowns(model);
                return View(model);
            }

            // Validation: Lesson exists
            var lessonExists = await _unitOfWork.Lessons.GetAsync(model.LessonId);
            if (lessonExists == null)
            {
                ModelState.AddModelError("LessonId", "درس انتخاب شده معتبر نیست.");
                await ReloadPresentationDropdowns(model);
                return View(model);
            }

            // Validation: Master exists
            var masterExists = await _unitOfWork.Masters.GetAsync(model.MasterId);
            if (masterExists == null)
            {
                ModelState.AddModelError("MasterId", "استاد انتخاب شده معتبر نیست.");
                await ReloadPresentationDropdowns(model);
                return View(model);
            }

            // Validation: Prevent duplicate presentations
            var exists = await _unitOfWork.Presentations
                .GetAll()
                .AnyAsync(p => p.LessonId == model.LessonId &&
                               p.MasterId == model.MasterId &&
                               p.DayHold == model.DayHold &&
                               p.StartTime == model.StartTime &&
                               !p.IsDeleted);

            if (exists)
            {
                ModelState.AddModelError("", "این ارائه قبلاً برای این درس و استاد ثبت شده است.");
                await ReloadPresentationDropdowns(model);
                return View(model);
            }

            try
            {
                // Add the presentation
                await _unitOfWork.Presentations.AddAsync(model);
                await _unitOfWork.CommitChangesAsync();

                TempData["SuccessMessage"] = "ارائه با موفقیت اضافه شد.";
                return RedirectToAction(nameof(Presentations));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطا در ذخیره ارائه: {ex.Message}");
                await ReloadPresentationDropdowns(model);
                return View(model);
            }
        }

        // Helper method to reload dropdowns if POST validation fails
        private async Task<IActionResult> ReloadPresentationDropdowns(Presentation model)
        {
            var lessons = await _unitOfWork.Lessons.GetAll().OrderBy(l => l.Name).ToListAsync();
            var masters = await _unitOfWork.Masters
                .GetAll()
                .Include(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .OrderBy(m => m.User.PersonInformation.LastName)
                .ToListAsync();

            ViewBag.Lessons = new SelectList(
                lessons.Select(l => new { l.Id, l.Name }),
                "Id",
                "Name",
                model.LessonId
            );

            ViewBag.Masters = new SelectList(
                masters.Select(m => new
                {
                    m.Id,
                    FullName = $"{m.User.PersonInformation.FirstName} {m.User.PersonInformation.LastName}"
                }),
                "Id",
                "FullName",
                model.MasterId
            );

            var persianWeekDays = new List<SelectListItem>
            {
                new SelectListItem { Value = "شنبه", Text = "شنبه" },
                new SelectListItem { Value = "یکشنبه", Text = "یکشنبه" },
                new SelectListItem { Value = "دوشنبه", Text = "دوشنبه" },
                new SelectListItem { Value = "سه‌شنبه", Text = "سه‌شنبه" },
                new SelectListItem { Value = "چهارشنبه", Text = "چهارشنبه" },
                new SelectListItem { Value = "پنج‌شنبه", Text = "پنج‌شنبه" },
                new SelectListItem { Value = "جمعه", Text = "جمعه" },
            };
            ViewBag.PersianWeekDays = new SelectList(persianWeekDays, "Value", "Text", model.DayHold);

            return View(model);
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
            var vm = new SelectionEventViewModel
            {
                Terms = _unitOfWork.Terms.GetAll()
                          .OrderByDescending(t => t.Year)
                          .ThenByDescending(t => t.Type)
                          .ToList(),
                SelectionStart = DateTime.Now,
                SelectionEnd = null
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddSelectionEvent(SelectionEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload terms for dropdown if validation fails
                model.Terms = _unitOfWork.Terms.GetAll()
                                .OrderByDescending(t => t.Year)
                                .ThenByDescending(t => t.Type)
                                .ToList();
                return View(model);
            }

            if (model.SelectionStart >= model.SelectionEnd)
            {
                ModelState.AddModelError("", "زمان شروع باید قبل از زمان پایان باشد.");
                model.Terms = _unitOfWork.Terms.GetAll()
                                .OrderByDescending(t => t.Year)
                                .ThenByDescending(t => t.Type)
                                .ToList();
                return View(model);
            }

            var selectionEvent = new SelectionTime
            {
                SelectionStart = model.SelectionStart,
                SelectionEnd = model.SelectionEnd ?? DateTime.Now,
                TermId = model.TermId
            };

            await _unitOfWork.SelectionTimes.AddAsync(selectionEvent);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction(nameof(SelectionEvents));
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

        #region Terms
        public async Task<IActionResult> AddNewTerm()
        {
            return View(new TermViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTerm(TermViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Validation: Start < End
            if (model.StartTime >= model.EndTime)
            {
                ModelState.AddModelError("", "تاریخ شروع باید قبل از تاریخ پایان باشد.");
                return View(model);
            }

            // Validation: Prevent duplicate terms
            var exists = await _unitOfWork.Terms
                .GetAll()
                .AnyAsync(t => t.Year == model.Year && t.Type == model.Type);

            if (exists)
            {
                ModelState.AddModelError("", "این ترم قبلاً تعریف شده است.");
                return View(model);
            }

            // Map ViewModel → Domain Model
            var term = new Term
            {
                Year = model.Year,
                Type = model.Type,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };

            await _unitOfWork.Terms.AddAsync(term);
            await _unitOfWork.CommitChangesAsync();

            return RedirectToAction(nameof(TermsList));
        }


        public IActionResult TermsList()
        {
            var terms = _unitOfWork.Terms
                .GetAll()
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.Type)
                .AsNoTracking()
                .ToList();

            return View(terms);
        }

    }
    #endregion
}
