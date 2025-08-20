using StockApp.Contracts;
using StockApp.Contracts.DTOs;
using StockApp.Models;
using StockApp.Services.Helpers;

namespace StockApp.Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrdersList;
        private readonly List<SellOrder> _sellOrdersList;

        public StocksService()
        {
            _buyOrdersList = new List<BuyOrder>();
            _sellOrdersList = new List<SellOrder>();
        }

        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest is null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderId = Guid.NewGuid();

            // Future Task to add order in Database.
            _buyOrdersList.Add(buyOrder);

            // Future return buyOrder.ToBuyOrderResponse().
            return Task.FromResult(buyOrder.ToBuyOrderResponse());
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest is null) throw new ArgumentNullException(); 

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderId = Guid.NewGuid();

            _sellOrdersList.Add(sellOrder);

            return Task.FromResult(sellOrder.ToSellOrderResponse());
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return Task.FromResult(_buyOrdersList.Select(o => o.ToBuyOrderResponse()).ToList());
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            return Task.FromResult(_sellOrdersList.Select(o => o.ToSellOrderResponse()).ToList());
        }
    }
}
