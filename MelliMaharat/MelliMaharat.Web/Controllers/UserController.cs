using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services;
using MelliMaharat.Models.Enums;
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

        public UserController(IUnitOfWork unitOfWork,
                              IAuthService authService)
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

            // Fetch user with PersonInformation
            var user = await _unitOfWork.Users
                .GetAll()
                .Include(u => u.PersonInformation)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            // Map to ViewModel
            var model = new UserDashboardViewModel
            {
                FullName = $"{user.PersonInformation.FirstName} {user.PersonInformation.LastName}",
                PhoneNumber = user.PersonInformation.PhoneNumber,
                AvatarId = user.AvatarId,
                Role = user.Role.ToString()
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
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = "رمز عبور با موفقیت تغییر یافت.";
            return RedirectToAction(nameof(Dashboard)); 
        }
    }
}
