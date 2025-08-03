using Microsoft.AspNetCore.Mvc;
using StockApp.Contracts;

namespace StockApp.Controllers
{
    [Controller]
    [Route("/")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;

        public TradeController(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
