using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StockApp.Contracts;
using StockApp.ViewModels;
using StockApp.Models.Options;
using StockApp.Contracts.DTOs;

namespace StockApp.Controllers
{
    [Controller]
    [Route("/")]
    [Route("trade")]
    public class TradeController : Controller
    {
        private readonly IStocksService _stocksService;
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;

        public TradeController(IFinnhubService finnhubService, IStocksService stocksService, IOptions<TradingOptions> options)
        {
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _tradingOptions = options.Value;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            StockTrade? stockTrade = null;
            _tradingOptions.DefaultStockSymbol = TempData["Trade"]?.ToString() ?? null;

            if (_tradingOptions.DefaultStockSymbol is null) _tradingOptions.DefaultStockSymbol = "MSFT";
            if (_tradingOptions.DefaultOrderQuantity is null) _tradingOptions.DefaultOrderQuantity = 100;

            ViewBag.DefaultOrderQuantity = _tradingOptions.DefaultOrderQuantity;

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

        [HttpGet("all-orders")]
        public async Task<IActionResult> AllOrders()
        {
            Orders orders = new Orders
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders(),
            };
            return View("Orders", orders);
        }

        [HttpPost("buy-order")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(p => p.Errors).Select(e => e.ErrorMessage).ToList();

                return View("Index");
            }
            await _stocksService.CreateBuyOrder(buyOrderRequest);
            return RedirectToAction("AllOrders", "Trade");
        }

        [HttpPost("sell-order")]
        public async Task<IActionResult> SellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View("Index");
            }

            await _stocksService.CreateSellOrder(sellOrderRequest);
            return RedirectToAction("AllOrders", "Trade");
        }

        [HttpGet("get-token")]
        public IActionResult GetToken()
        {
            return Ok(new { token = _tradingOptions.ApiKey! });
        }

        [HttpGet("orders-pdf")]
        public async Task<IActionResult> OrdersPDF()
        {
            Orders orders = new Orders()
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders(),
            };

            return new ViewAsPdf("OrdersPDF", orders)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 20, 20, 20)
            };
        }
    }
}
