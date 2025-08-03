using Microsoft.Extensions.Options;
using StockApp.Models.Options;
using StockApp.Contracts;
using System.Text.Json;

namespace StockApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly TradingOptions _tradingOptions;

        public FinnhubService(IHttpClientFactory httpClientFactory, IOptions<TradingOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _tradingOptions = options.Value;
        }

        public async override Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            Dictionary<string, object>? response = new Dictionary<string, object>();

            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/stock/profile2?symbol={stockSymbol}&token={_tradingOptions.ApiKey!}"),
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            Stream stream = responseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = reader.ReadToEnd();
            response = JsonSerializer.Deserialize<Dictionary<string, object>?>(jsonResponse);

            return response;
        }

        public async override Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            Dictionary<string, object>? response = new Dictionary<string, object>();

            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_tradingOptions.FinnhubConnection!}/quote?symbol={stockSymbol}&token={_tradingOptions.ApiKey!}"),
            };

            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            Stream stream = responseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string jsonResponse = reader.ReadToEnd();
            response = JsonSerializer.Deserialize<Dictionary<string, object>?>(jsonResponse);

            return response;
        }
    }
}
