using Microsoft.EntityFrameworkCore;
using StockApp.Contracts;
using StockApp.Contracts.DTOs;
using StockApp.Models;
using StockApp.Services.Helpers;

namespace StockApp.Services
{
    public class StocksService : IStocksService
    {
        private StocksMarketDbContext _dbContext;

        public StocksService(StocksMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest is null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderId = Guid.NewGuid();

            // Add order in Database.
            _dbContext.BuyOrders.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest is null) throw new ArgumentNullException(); 

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderId = Guid.NewGuid();

            _dbContext.SellOrders.Add(sellOrder);
            await _dbContext.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return await _dbContext.BuyOrders.Select(o => o.ToBuyOrderResponse()).ToListAsync();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return await _dbContext.SellOrders.Select(o => o.ToSellOrderResponse()).ToListAsync();
        }
    }
}
