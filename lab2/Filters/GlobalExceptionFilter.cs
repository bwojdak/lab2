using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using lab2.Models;
using System.Diagnostics;

namespace lab2.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _log;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> log)
    {
        _log = log;
    }

    public void OnException(ExceptionContext context)
    {
        _log.LogError(
            context.Exception,
            "Wystąpił wyjątek podczas obsługi żądania");

        context.HttpContext.Response.StatusCode = 500;

        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id
                        ?? context.HttpContext.TraceIdentifier
        };

        context.Result = new ViewResult
        {
            ViewName = "/Views/Shared/Error.cshtml",
            ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorViewModel>(
                new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                context.ModelState)
            {
                Model = model
            },
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}