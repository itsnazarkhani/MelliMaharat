using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using Microsoft.AspNetCore.Mvc;

public class MasterController : Controller
{
    [AuthorizeByRole(UserRoles.Master)]
    public IActionResult PresentedLessons() => View();
}
