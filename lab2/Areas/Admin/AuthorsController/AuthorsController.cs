using Microsoft.AspNetCore.Mvc;

namespace lab2.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorsController : Controller
{
    private static List<string> authors = new()
    {
        "Adam Mickiewicz",
        "Henryk Sienkiewicz",
        "Bolesław Prus"
    };

    public IActionResult Index()
    {
        return View(authors);
    }

    [HttpPost]
    public IActionResult Create(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            authors.Add(name);
        }

        return RedirectToAction(nameof(Index));
    }
}