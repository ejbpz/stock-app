using StockApp.Contracts.DTOs;

namespace StockApp.Contracts
{
    public interface IStocksGetterService
    {
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
