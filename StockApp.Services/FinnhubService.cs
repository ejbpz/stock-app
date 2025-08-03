using Microsoft.Extensions.Options;
using StockApp.Models.Options;
using StockApp.Contracts;

namespace StockApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly TradingOptions _tradingOptions;

        public FinnhubService(IHttpClientFactory httpClientFactory, IOptions<TradingOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _tradingOptions = options.Value;
        }

        public override Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public override Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
