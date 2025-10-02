namespace StockApp.Models.Options
{
    /// <summary>
    /// Options from configurations.
    /// </summary>
    public class TradingOptions
    {
        public int? DefaultOrderQuantity { get; set; }
        public string? DefaultStockSymbol { get; set; }
        public string? FinnhubConnection { get; set; }
        public string? ApiKey { get; set; }
        public List<string> Top25PopularStocks { get; set; } = new List<string>();
    }
}
