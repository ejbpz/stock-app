using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using AutoFixture;
using FluentAssertions;
using StockApp.Contracts;
using StockApp.ViewModels;
using StockApp.Controllers;
using StockApp.Models.Options;
using Microsoft.Extensions.Logging;

namespace StockApp.Test
{
    public class StocksControllerTest
    {
        private readonly IFinnhubService _finnhubService;
        private readonly Mock<IFinnhubService> _mockFinnhubService;

        private readonly ILogger<TradeController> _logger;
        private readonly Mock<ILogger<TradeController>> _mockLogger;

        private readonly IOptions<TradingOptions> _options;

        private readonly Fixture _fixture;

        public StocksControllerTest()
        {
            _fixture = new Fixture();

            _options = Options.Create(
                _fixture.Build<TradingOptions>()
                .With(o => o.Top25PopularStocks, "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO")
                .Create()
            );

            _mockFinnhubService = new Mock<IFinnhubService>();
            _finnhubService = _mockFinnhubService.Object;

            _mockLogger = new Mock<ILogger<TradeController>>();
            _logger = _mockLogger.Object;
        }

        #region Explore
        [Fact]
        public async Task Explore_ReturnViewWithStocksList()
        {
            // Arrange
            StocksController _stocksController = new StocksController(_options, _finnhubService, _logger);

            List<Dictionary<string, string>> stocksDictionary = new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>()
                {
                    { "symbol", "AAPL" },
                    { "description", "Apple Inc" }
                },
                new Dictionary<string, string>()
                {
                    { "symbol", "MSFT" },
                    { "description", "Microsoft Corporation" }
                },
                new Dictionary<string, string>()
                {
                    { "symbol", "AMZN" },
                    { "description", "Amazon.com Inc" }
                },
                new Dictionary<string, string>()
                {
                    { "symbol", "TSLA" },
                    { "description", "Tesla Inc" }
                },
                new Dictionary<string, string>()
                {
                    { "symbol", "GOOGL" },
                    { "description", "Alphabet Inc Class C" }
                },
            };

            List<Stock> stocks = new List<Stock>()
            {
                new Stock()
                {
                    StockSymbol = "AAPL",
                    StockName = "Apple Inc",
                },
                new Stock()
                {
                    StockName = "Microsoft Corporation",
                    StockSymbol = "MSFT",
                },
                new Stock()
                {
                    StockName = "Amazon.com Inc",
                    StockSymbol = "AMZN",
                },
                new Stock()
                {
                    StockName = "Tesla Inc",
                    StockSymbol = "TSLA",
                },
                new Stock()
                {
                    StockName = "Alphabet Inc Class C",
                    StockSymbol = "GOOGL",
                },
            };

            _mockFinnhubService.Setup(m => m
                .GetStocks())
                .ReturnsAsync(stocksDictionary);

            // Act
            var result = await _stocksController.Explore();

            // Assert
            ViewResult viewResult = result.Should().BeOfType<ViewResult>().Which;

            viewResult.Should().NotBeNull();
            viewResult.ViewData.Model.Should().NotBe(new List<Stock>());
            viewResult.ViewData.Model.Should().BeAssignableTo<List<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(stocks);
        }
        #endregion
    }
}