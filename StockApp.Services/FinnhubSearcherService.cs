using StockApp.Contracts;
using StockApp.RepositoryContracts;

namespace StockApp.Services
{
    public class FinnhubSearcherService : IFinnhubSearcherService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubSearcherService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
        }
    }
}
