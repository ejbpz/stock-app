using StockApp.Models;
using StockApp.Contracts;
using StockApp.Contracts.DTOs;
using StockApp.Services.Helpers;
using StockApp.RepositoryContracts;

namespace StockApp.Services
{
    public class StocksAdderService : IStocksAdderService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksAdderService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest is null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderId = Guid.NewGuid();

            // Add order in Database.
            await _stocksRepository.CreateBuyOrder(buyOrder);
            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest is null) throw new ArgumentNullException(); 

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderId = Guid.NewGuid();

            // Add order in Database.
            await _stocksRepository.CreateSellOrder(sellOrder);
            return sellOrder.ToSellOrderResponse();
        }
    }
}
