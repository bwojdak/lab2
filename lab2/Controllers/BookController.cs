using lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers;

public class BookController : Controller
{
    public IActionResult Index()
    {
        return View(BookStoreMemory.Books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
    }
    BookStoreMemory.Books.Add()
    return RedirectToAction(nameof(Index));

}