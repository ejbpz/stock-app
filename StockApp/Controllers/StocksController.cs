using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Contracts;
using StockApp.Models.Options;

namespace StockApp.Controllers
{
    [Controller]
    [Route("stocks")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _options;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;

        public StocksController(IOptions<TradingOptions> configureOptions, IFinnhubService finnhubService, IStocksService stocksService)
        {
            _options = configureOptions.Value;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
        }

        [HttpGet("explore")]
        public IActionResult Explore()
        {
            return View();
        }
    }
}
