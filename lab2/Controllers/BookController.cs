using lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace lab2.Controllers;
using lab2.Filters;
[ServiceFilter(typeof(LogActionFilter))]
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
        if (!ModelState.IsValid)
            return View(model);

        BookStoreMemory.Books.Add(new Book());

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ByYear(int year)
    {
        var books = BookStoreMemory.Books.
            Where(b => b.PublicationDate?.Year == year)
            .ToList();

        return View("Index", books);
    }

    public IActionResult ByIsbn(string isbn)
    {
        var book = BookStoreMemory.Books
            .FirstOrDefault(b => b.Isbn == isbn);

        if (book == null)
            return NotFound();

        return RedirectToAction("Details", new { id = book.Id });
    }

    [HttpGet("Book/Details/{id:int}")]
    public IActionResult Details(int id)
    {
        var book = BookStoreMemory.Books
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound();

        return View(book);
    }
    public IActionResult Docs(string? path)
    {
        ViewBag.Path = path;
        return View();
    }
    public IActionResult Boom()
    {
        throw new InvalidOperationException("Celowy wyjątek");
    }
}