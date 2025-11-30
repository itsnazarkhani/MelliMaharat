using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using Microsoft.AspNetCore.Mvc;

public class StudentController : Controller
{
    [AuthorizeByRole(UserRoles.Student)]
    public IActionResult TermLessons() => View();

    public IActionResult SelectUnits() => View();
    public IActionResult Grades() => View();
    public IActionResult Attendance() => View();
}
