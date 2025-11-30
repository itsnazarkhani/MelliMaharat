using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    [AuthorizeByRole(UserRoles.Admin)]
    public IActionResult Lessons() => View();

    public IActionResult Masters() => View();
    public IActionResult Students() => View();
    public IActionResult Presentations() => View();
    public IActionResult CreateEvent() => View();
}
