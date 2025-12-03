using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services;
using MelliMaharat.Models.Enums;
using MelliMaharat.Models.Helpers;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels;
using MelliMaharat.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MelliMaharat.Web.Controllers
{
    [AuthorizeByRole(UserRoles.Student, UserRoles.Master, UserRoles.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public UserController(
            IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<IActionResult> Dashboard()
        {
            // Get logged-in user's ID from claims
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            // Fetch user including PersonInformation
            var user = await _unitOfWork.Users
                .GetAll()
                .Include(u => u.PersonInformation)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var person = user.PersonInformation;

            // Map to ViewModel with null-safety
            var model = new UserDashboardViewModel
            {
                FullName = person != null
                    ? $"{person.FirstName ?? ""} {person.LastName ?? ""}".Trim()
                    : "نامشخص",

                PhoneNumber = person?.PhoneNumber ?? "ثبت نشده",

                AvatarId = user.AvatarId,

                Role = user.Role.GetDescription(),

                NationalCode = person?.NationalCode ?? "ثبت نشده",

                Email = user.Email ?? "ثبت نشده",

                BirthDate = person != null
                    ? person.BirthDate.ToString("yyyy/MM/dd")
                    : "ثبت نشده"
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var username = User.Identity?.Name;
            if (username == null)
                return Unauthorized();

            var result = await _authService.ChangePasswordAsync(username, model.CurrentPassword, model.NewPassword);

            if (!result.IsSuccess)
            {
                TempData["IncorrectCurrentPassword"] = result.Message;
                return View(model);
            }

            TempData["SuccessMessage"] = "رمز عبور با موفقیت تغییر یافت.";
            return RedirectToAction(nameof(Dashboard)); 
        }

        public async Task<IActionResult> EditProfile()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _unitOfWork.Users.GetAll()
                .Include(u => u.PersonInformation) 
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return NotFound();

            var model = new EditProfileViewModel
            {
                FirstName = user.PersonInformation.FirstName,
                LastName = user.PersonInformation.LastName,
                BirthDate = user.PersonInformation.BirthDate.ToString("yyyy/MM/dd"),
                NationalCode = user.PersonInformation.NationalCode,
                PhoneNumber = user.PersonInformation.PhoneNumber,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _unitOfWork.Users.GetAll()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return NotFound();

            // Update only the email
            user.Email = model.Email;

            await _unitOfWork.CommitChangesAsync();

            TempData["EditProfileSuccessMessage"] = "ایمیل با موفقیت به‌روزرسانی شد.";

            return RedirectToAction("EditProfile");
        }
    }
}
