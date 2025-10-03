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

        public StocksController(IOptions<TradingOptions> configureOptions, IFinnhubService finnhubService)
        {
            _tradingOptions = configureOptions.Value;
            _finnhubService = finnhubService;
        }

        [HttpGet("explore")]
        public async Task<IActionResult> Explore()
        {
            List<Dictionary<string, string>>? stocksResponse = await _finnhubService.GetStocks();
            List<Stock> stocks = new List<Stock>();

            ViewBag.SelectedStock = _tradingOptions.DefaultStockSymbol;

            if(stocksResponse is not null)
            {
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

            return View(stocks);
        }

        [HttpGet("stock-details/{stock}")]
        public IActionResult Details(string stock)
        {
            return ViewComponent("SelectedStock", new { stockSymbolToSearch = stock });
        }
    }
}
