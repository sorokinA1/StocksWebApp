using Microsoft.AspNetCore.Mvc;
using StocksWebApp.Services;

namespace StocksWebApp.Controllers;

public class HomeController : Controller
{
    private readonly FinnhubService _finnhubService;

    public HomeController(FinnhubService finnhubService)
    {
        _finnhubService = finnhubService;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        await _finnhubService.CreateClient();
        return View();
    }
}