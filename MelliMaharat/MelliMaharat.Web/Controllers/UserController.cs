using Microsoft.AspNetCore.Mvc;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;

namespace MelliMaharat.Web.Controllers
{
    [AuthorizeByRole(UserRoles.Student, UserRoles.Master, UserRoles.Admin)]
    public class UserController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
