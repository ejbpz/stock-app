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
    }
}
