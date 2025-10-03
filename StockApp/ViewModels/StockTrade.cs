namespace StockApp.ViewModels
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double? Price { get; set; }
        public uint Quantity { get; set; } = 0;
        public string? Logo { get; set; }
        public string? Exchange { get; set; }
        public string? Industry { get; set; }
    }
}
