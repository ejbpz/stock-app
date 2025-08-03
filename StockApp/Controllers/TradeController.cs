using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Contracts;
using StockApp.Models.Options;
using StockApp.Models.ViewModels;

namespace StockApp.Controllers
{
    [Controller]
    [Route("/")]
    [Route("trade")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;

        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options)
        {
            _finnhubService = finnhubService;
            _tradingOptions = options.Value;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            StockTrade? stockTrade = null;

            if (_tradingOptions.DefaultStockSymbol is null) _tradingOptions.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol!);
            Dictionary<string, object>? priceQuote = await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol!);

            if (companyProfile is not null && priceQuote is not null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = Convert.ToString(companyProfile["ticker"]),
                    StockName = Convert.ToString(companyProfile["name"]),
                    Price = Convert.ToDouble(priceQuote["c"].ToString()),
                };
            }

            return View(stockTrade);
        }

        [HttpGet("get-token")]
        public IActionResult GetToken()
        {
            return Ok(new { token = _tradingOptions.ApiKey!, symbol = _tradingOptions.DefaultStockSymbol! });
        }
    }
}
