using Microsoft.Extensions.Options;
using StockApp.Contracts;
using System.Net.Http;

namespace StockApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        //private readonly IOptions<>

        public FinnhubService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
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
