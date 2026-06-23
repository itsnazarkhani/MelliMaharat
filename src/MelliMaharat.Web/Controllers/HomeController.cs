using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MelliMaharat.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy() => View();
}
