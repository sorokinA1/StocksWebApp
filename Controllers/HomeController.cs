using Microsoft.AspNetCore.Mvc;
using StocksWebApp.Services;

namespace StocksWebApp.Controllers;

public class HomeController : Controller
{
    private readonly NewService _newService;

    public HomeController(NewService newService)
    {
        _newService = newService;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        await _newService.CreateClient();
        return View();
    }
}