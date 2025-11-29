namespace StockApp.RepositoryContracts
{
    public interface IFinnhubRepository
    {
        /// <summary>
        /// Get company's profile.
        /// </summary>
        /// <param name="stockSymbol">Company stock symbol.</param>
        /// <returns>Returns dictionary with the company profile.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Get company's stock price.
        /// </summary>
        /// <param name="stockSymbol">Company stock symbol.</param>
        /// <returns>Returns dictionary with the stock price.</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

        /// <summary>
        /// Get a bunch of companies stocks.
        /// </summary>
        /// <returns>List of dictionary with stocks.</returns>
        Task<List<Dictionary<string, string>>?> GetStocks();

        /// <summary>
        /// Search and specific stock company.
        /// </summary>
        /// <param name="stockSymbolToSearch">Company stock symbol.</param>
        /// <returns>Returns the data of this specific company.</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
