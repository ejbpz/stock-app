namespace StockApp.Models
{
    /// <summary>
    /// Domain model for SellOrder.
    /// </summary>
    public class SellOrder
    {
        public Guid SellOrderId { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
    }
}
