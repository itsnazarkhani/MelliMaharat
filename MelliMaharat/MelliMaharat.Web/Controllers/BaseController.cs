using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MelliMaharat.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Guid CurrentUserId
        {
            get
            {
                var claim = User.FindFirst(ClaimTypes.NameIdentifier);
                return claim != null && Guid.TryParse(claim.Value, out var userId) ? userId : Guid.Empty;
            }
        }

        protected string CurrentUsername
        {
            get => User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }

        protected UserRoles? CurrentUserRole
        {
            get
            {
                var role = User.FindFirst("Role")?.Value;
                return role != null && Enum.TryParse<UserRoles>(role, out var userRole) ? userRole : null;
            }
        }

        protected bool IsUserAuthenticated
        {
            get => User.Identity?.IsAuthenticated ?? false;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (IsUserAuthenticated)
            {
                ViewBag.CurrentUserId = CurrentUserId;
                ViewBag.CurrentUsername = CurrentUsername;
                ViewBag.CurrentUserRole = CurrentUserRole;
            }
        }
    }
}
