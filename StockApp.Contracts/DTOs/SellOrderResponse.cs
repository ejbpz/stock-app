using StockApp.Models;

namespace StockApp.Contracts.DTOs
{
    /// <summary>
    /// DTO Response when we sell an order.
    /// </summary>
    public class SellOrderResponse
    {
        public Guid SellOrderId { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }
        public uint? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TradeAmount { get; set; }

        public override string ToString()
        {
            return $"SellOrderId: {SellOrderId}" +
                $"\n\tStockSymbol: {StockSymbol}" +
                $"\n\tStockName: {StockName}" +
                $"\n\tDateAndTimeOfOrder: {DateAndTimeOfOrder?.ToString("dd/MMMM/yyyy")}" +
                $"\n\tQuantity: {Quantity}" +
                $"\n\tPrice: {Price}" +
                $"\n\tTradeAmount: {TradeAmount}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != typeof(SellOrderResponse)) return false;

            SellOrderResponse sellOrder = (SellOrderResponse)obj;

            return SellOrderId == sellOrder.SellOrderId &&
                StockSymbol == sellOrder.StockSymbol &&
                StockName == sellOrder.StockName &&
                DateAndTimeOfOrder == sellOrder.DateAndTimeOfOrder &&
                Quantity == sellOrder.Quantity &&
                Price == sellOrder.Price &&
                TradeAmount == sellOrder.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                Price = sellOrder.Price,
                Quantity = sellOrder.Quantity,
                StockName = sellOrder.StockName,
                StockSymbol = sellOrder.StockSymbol,
                SellOrderId = sellOrder.SellOrderId,
                TradeAmount = sellOrder.Price * sellOrder.Quantity,
            };
        }
    }
}
