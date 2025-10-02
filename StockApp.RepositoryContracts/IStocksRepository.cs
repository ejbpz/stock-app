using StockApp.Models;

namespace StockApp.RepositoryContracts
{
    public interface IStocksRepository
    {
        /// <summary>
        /// Create a buy order into database.
        /// </summary>
        /// <param name="buyOrder">Object order to be added.</param>
        /// <returns>Returns the same order added.</returns>
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        /// <summary>
        /// Create a sell order into database.
        /// </summary>
        /// <param name="sellOrder">Object order to be added.</param>
        /// <returns>Returns the same order added.</returns>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// Retrieve a list of orders that has been bought.
        /// </summary>
        /// <returns>Returns a list of orders.</returns>
        Task<List<BuyOrder>> GetBuyOrders();

        /// <summary>
        /// Retrieve a list of orders that has been sold.
        /// </summary>
        /// <returns>Returns a list of orders.</returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}
