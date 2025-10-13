using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Contracts;
using StockApp.ViewModels;
using StockApp.Models.Options;

namespace StockApp.Controllers
{
    [Controller]
    [Route("stocks")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly ILogger<TradeController> _logger;

        public StocksController(IOptions<TradingOptions> configureOptions, IFinnhubService finnhubService, ILogger<TradeController> logger)
        {
            _tradingOptions = configureOptions.Value;
            _finnhubService = finnhubService;
            _logger = logger;
        }

        [HttpGet("explore")]
        public async Task<IActionResult> Explore()
        {
            _logger.LogInformation("User into ExploreAction in StocksController");

            List<Dictionary<string, string>>? stocksResponse = await _finnhubService.GetStocks();
            List<Stock> stocks = new List<Stock>();

            ViewBag.SelectedStock = _tradingOptions.DefaultStockSymbol;

            if(stocksResponse is not null)
            {
                _logger.LogInformation("Getting the popular stocks");
                List<string> symbols = _tradingOptions.Top25PopularStocks!.Split(",").ToList();

                foreach(Dictionary<string, string> stockResponse in stocksResponse)
                {
                    if (symbols.Contains(stockResponse["symbol"]))
                    {
                        Stock stock = new Stock() 
                        {
                            StockName = stockResponse["description"],
                            StockSymbol = stockResponse["symbol"],
                        };
                    
                        stocks.Add(stock);
                    }

                }
            }

            _logger.LogInformation("Return to ExploreView with the 25 Popular Stocks");
            return View(stocks);
        }

        [HttpGet("stock-details/{stock}")]
        public IActionResult Details(string stock)
        {
            _logger.LogInformation("User into DetailsAction in StocksController");
            _logger.LogInformation("Return ViewComponent to UI");
            return ViewComponent("SelectedStock", new { stockSymbolToSearch = stock });
        }
    }
}
