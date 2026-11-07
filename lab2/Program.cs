using lab2.Middleware;
using lab2.Filters;
var builder = WebApplication.CreateBuilder(args);

// Rejestracja MVC
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddScoped<LogActionFilter>();
builder.Services.AddSingleton<RequestStats>();

var app = builder.Build();
app.UseMiddleware<RequestTimingMiddleware>();

// Obsługa błędów
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

// CZĘŚĆ A - TRASY KSIĄŻEK
app.MapControllerRoute(
    name: "books-by-year",
    pattern: "books/year/{year:int:min(1450)}",
    defaults: new { controller = "Book", action = "ByYear" });

app.MapControllerRoute(
    name: "books-by-isbn",
    pattern: @"books/isbn/{isbn:regex(^\d{{13}}$)}",
    defaults: new { controller = "Book", action = "ByIsbn" });

app.MapControllerRoute(
    name: "docs",
    pattern: "docs/{*path}",
    defaults: new { controller = "Home", action = "Docs" });

// CZĘŚĆ C - OBSZAR ADMIN
app.MapControllerRoute(
    name: "admin-area",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// TRASA DOMYŚLNA
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();