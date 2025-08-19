namespace StockApp.Contracts
{
    public interface IFinnhubService
    {
        /// <summary>
        /// Get company's profile by its stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Company's symbol</param>
        /// <returns>Returns a dictionary with the company details (country, currency, logo...).</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Get company's prices in stock market.
        /// </summary>
        /// <param name="stockSymbol">Company's symbol</param>
        /// <returns>Returns a dictionary with pricing details ()</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
