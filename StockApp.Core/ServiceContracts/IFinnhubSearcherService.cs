namespace StockApp.Contracts
{
    public interface IFinnhubSearcherService
    {
        /// <summary>
        /// Search the stocks for a company.
        /// </summary>
        /// <param name="stockSymbolToSearch">Company's symbol.</param>
        /// <returns>Returns a dictionary of the stock,</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
