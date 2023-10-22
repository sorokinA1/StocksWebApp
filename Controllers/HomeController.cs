using Microsoft.AspNetCore.Mvc;

namespace StocksWebApp.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}