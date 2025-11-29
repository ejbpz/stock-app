using Microsoft.EntityFrameworkCore;
using StockApp.Models;
using StockApp.RepositoryContracts;

namespace StockApp.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly StocksMarketDbContext _marketDbContext;

        public StocksRepository(StocksMarketDbContext marketDbContext)
        {
            _marketDbContext = marketDbContext;
        }

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _marketDbContext.BuyOrders.Add(buyOrder);
            await _marketDbContext.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _marketDbContext.SellOrders.Add(sellOrder);
            await _marketDbContext.SaveChangesAsync();

            return sellOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return await _marketDbContext.BuyOrders.ToListAsync();
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return await _marketDbContext.SellOrders.ToListAsync();
        }
    }
}
