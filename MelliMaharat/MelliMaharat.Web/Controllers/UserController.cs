using Microsoft.AspNetCore.Mvc;
using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Web.Filters;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MelliMaharat.Web.Controllers
{
    [AuthorizeByRole(UserRoles.Student, UserRoles.Master, UserRoles.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
