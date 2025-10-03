using Microsoft.AspNetCore.Mvc;
using StockApp.Contracts;
using StockApp.ViewModels;

namespace StockApp.ViewComponents
{
    [ViewComponent]
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubService _finnhubService;

        public SelectedStockViewComponent(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbolToSearch)
        {
            StockTrade? stockTrade = null;

            var companyProfile = await _finnhubService.GetCompanyProfile(stockSymbolToSearch ?? "");
            var companyQuote = await _finnhubService.GetStockPriceQuote(stockSymbolToSearch ?? "");

            if(companyProfile is not null && companyQuote is not null)
            {
                stockTrade = new StockTrade()
                {
                    StockName = Convert.ToString(companyProfile["name"]),
                    StockSymbol = Convert.ToString(companyProfile["ticker"]),
                    Logo = Convert.ToString(companyProfile["logo"]),
                    Exchange = Convert.ToString(companyProfile["exchange"]),
                    Industry = Convert.ToString(companyProfile["finnhubIndustry"]),
                    Price = Convert.ToDouble(companyQuote["c"].ToString()),
                };
            }

            return View("SelectedStock", stockTrade);
        }
    }
}
