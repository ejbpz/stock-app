namespace StockApp.Contracts
{
    public abstract class IFinnhubService
    {
        public abstract Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
        public abstract Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
