using StockApp.Contracts.DTOs;

namespace StockApp.Contracts
{
    public interface IStocksService
    {
        /// <summary>
        /// Inserts a new buy order to the database.
        /// </summary>
        /// <param name="buyOrderRequest">New buy order to be added.</param>
        /// <returns>Returns a new buy order with an ID.</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Inserts a new sell order to the database.
        /// </summary>
        /// <param name="sellOrderRequest">New sell order to be added.</param>
        /// <returns>Returns a new sell order with an ID.</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Get the existing list of buy orders from database.
        /// </summary>
        /// <returns>Returns a list of buy orders.</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders();

        /// <summary>
        /// Get the existing list of sell orders from database.
        /// </summary>
        /// <returns>Returns a list of sell orders.</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
