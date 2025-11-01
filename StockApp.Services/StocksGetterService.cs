using StockApp.Contracts;
using StockApp.Contracts.DTOs;
using StockApp.RepositoryContracts;

namespace StockApp.Services
{
    public class StocksGetterService : IStocksGetterService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksGetterService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return (await _stocksRepository.GetBuyOrders()).Select(o => o.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return (await _stocksRepository.GetSellOrders()).Select(o => o.ToSellOrderResponse()).ToList();
        }
    }
}
