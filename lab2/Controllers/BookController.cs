using lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers;

public class BookController : Controller
{
    public IActionResult Index()
    {
        return View(BookStoreMemory.Books);
    }
}