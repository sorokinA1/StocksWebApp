using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksWebApp.Services;

namespace StocksWebApp.Controllers;

public class HomeController : Controller
{
    private readonly FinnhubService _finnhubService;
    private readonly IOptions<TradingOptions> _tradingOptions;

    public HomeController(
        FinnhubService finnhubService, 
        IOptions<TradingOptions> tradingOptions)
    {
        _finnhubService = finnhubService;
        _tradingOptions = tradingOptions;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var responseDict = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
        return View();
    }
}