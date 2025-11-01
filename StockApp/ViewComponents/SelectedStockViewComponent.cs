using Microsoft.AspNetCore.Mvc;
using StockApp.Contracts;
using StockApp.ViewModels;

namespace StockApp.ViewComponents
{
    [ViewComponent]
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubGetterService _finnhubGetterService;

        public SelectedStockViewComponent(IFinnhubGetterService finnhubService)
        {
            _finnhubGetterService = finnhubService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbolToSearch)
        {
            StockTrade? stockTrade = null;

            var companyProfile = await _finnhubGetterService.GetCompanyProfile(stockSymbolToSearch ?? "");
            var companyQuote = await _finnhubGetterService.GetStockPriceQuote(stockSymbolToSearch ?? "");

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
