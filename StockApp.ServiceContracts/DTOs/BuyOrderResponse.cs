using StockApp.Models;

namespace StockApp.Contracts.DTOs
{
    /// <summary>
    /// DTO Response when we buy an order.
    /// </summary>
    public class BuyOrderResponse
    {
        public Guid BuyOrderId { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }
        public uint? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TradeAmount { get; set; }

        public override string ToString()
        {
            return $"BuyOrderId: {BuyOrderId}" +
                $"\n\tStockSymbol: {StockSymbol}" +
                $"\n\tStockName: {StockName}" +
                $"\n\tDateAndTimeOfOrder: {DateAndTimeOfOrder?.ToString("dd/MMMM/yyyy")}" +
                $"\n\tQuantity: {Quantity}" +
                $"\n\tPrice: {Price}" +
                $"\n\tTradeAmount: {TradeAmount}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != typeof(BuyOrderResponse)) return false;

            BuyOrderResponse buyOrder = (BuyOrderResponse)obj;

            return BuyOrderId == buyOrder.BuyOrderId &&
                StockSymbol == buyOrder.StockSymbol &&
                StockName == buyOrder.StockName &&
                DateAndTimeOfOrder == buyOrder.DateAndTimeOfOrder &&
                Quantity == buyOrder.Quantity &&
                Price == buyOrder.Price &&
                TradeAmount == buyOrder.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                StockName = buyOrder.StockName,
                BuyOrderId = buyOrder.BuyOrderId,
                StockSymbol = buyOrder.StockSymbol,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                TradeAmount = buyOrder.Price * buyOrder.Quantity,
            };
        }
    }
}
