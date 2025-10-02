using Microsoft.Extensions.Options;
using System.Text.Json;
using StockApp.RepositoryContracts;
using StockApp.Models.Options;

namespace StockApp.Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        private readonly HttpClient _httpClient;
        private readonly TradingOptions _tradingOptions;

        public FinnhubRepository(IHttpClientFactory httpClientFactory, IOptions<TradingOptions> options)
        {
            _tradingOptions = options.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/stock/profile2?symbol={stockSymbol}&token={_tradingOptions.ApiKey!}"),
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            Stream stream = await responseMessage.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = await reader.ReadToEndAsync();
            
            return JsonSerializer.Deserialize<Dictionary<string, object>?>(jsonResponse);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/quote?symbol={stockSymbol}&token={_tradingOptions.ApiKey!}")
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            Stream stream = await responseMessage.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<Dictionary<string, object>?>(jsonResponse);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/stock/symbol?exchange=US&token={_tradingOptions.ApiKey!}")
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);
            
            Stream stream = await responseMessage.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<List<Dictionary<string, string>>?>(jsonResponse);
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/search?q={stockSymbolToSearch}&token={_tradingOptions.ApiKey!}")
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            Stream stream = await responseMessage.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<Dictionary<string, object>?>(jsonResponse);
        }
    }
}
