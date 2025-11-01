using StockApp.Contracts;
using StockApp.RepositoryContracts;

namespace StockApp.Services
{
    public class FinnhubGetterService : IFinnhubGetterService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubGetterService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            return await _finnhubRepository.GetCompanyProfile(stockSymbol);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            return await _finnhubRepository.GetStocks();
        }
    }
}
