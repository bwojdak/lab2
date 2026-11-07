using Microsoft.AspNetCore.Mvc;

namespace lab2.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}