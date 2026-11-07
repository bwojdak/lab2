
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using lab2.Models;

namespace lab2.Controllers;

public class HomeController : Controller
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorProvider;

    public HomeController(
        IActionDescriptorCollectionProvider actionDescriptorProvider)
    {
        _actionDescriptorProvider = actionDescriptorProvider;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Część A - dokumentacja
    public IActionResult Docs(string? path)
    {
        ViewBag.Path = path;
        return View();
    }

    // Część B1 - tabela tras
    public IActionResult Routes()
    {
        var routes = _actionDescriptorProvider
            .ActionDescriptors.Items
            .Select(action =>
            {
                var controller = action.RouteValues["controller"];
                var actionName = action.RouteValues["action"];

                var methods = action.ActionConstraints?
                    .OfType<HttpMethodActionConstraint>()
                    .SelectMany(c => c.HttpMethods)
                    .Distinct()
                    .ToList();

                return new
                {
                    Url = action.AttributeRouteInfo?.Template
                          ?? "(trasa konwencjonalna)",
                    ControllerAction = $"{controller}.{actionName}",
                    HttpMethod = methods != null && methods.Any()
                        ? string.Join(", ", methods)
                        : "Dowolna"
                };
            })
            .ToList();

        return View(routes);
    }

    // Część E - test wyjątku
    public IActionResult Boom()
    {
        throw new InvalidOperationException("Celowy wyjątek");
    }

    // Część F - obsługa kodów HTTP
    public IActionResult HttpStatus(int code)
    {
        Response.StatusCode = code;
        ViewBag.Code = code;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
