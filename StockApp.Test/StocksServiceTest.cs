using StockApp.Contracts.DTOs;
using StockApp.Services;

namespace StockApp.Test
{
    public class StocksServiceTest
    {
        private readonly StocksService _stocksService;

        public StocksServiceTest(StocksService stocksService)
        {
            _stocksService = stocksService;
        }

        private BuyOrderRequest CreateBuyOrderRequest()
        {
            return new BuyOrderRequest()
            {
                Quantity = 23,
                Price = 340.34,
                StockSymbol = "MSFT",
                StockName = "Microsoft Corp",
                DateAndTimeOfOrder = DateTime.Now,
            };
        }

        private SellOrderRequest CreateSellOrderRequest()
        {
            return new SellOrderRequest()
            {
                Quantity = 72,
                Price = 506.87,
                StockSymbol = "IBM",
                DateAndTimeOfOrder = DateTime.Now,
                StockName = "International Business Machines Corp",
            };
        }

        #region CreateBuyOrder
        // If we supply a null parameter, it should throw ArgumentNullException.
        [Fact]
        public void CreateBuyOrder_NullRequest()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply Quantity less than 1, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityLessMinimum()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.Quantity = 0;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply Quantity higher than 100_000, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityHigherMaximum()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.Quantity = 100_001;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply Price less than 1, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceLessMinimum()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.Price = 0;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply Price higher than 10_000, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceHigherMaximum()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.Price = 10_001;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply StockSymbol as null, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_NullStockSymbol()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.StockSymbol = null;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply DateAndTimeOfOrder before minimum, it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateAndTimeOfOrderBeforeMinimum()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            buyOrderRequest.DateAndTimeOfOrder = new DateTime(1999, 12, 31);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply a proper BuyOrderRequest, it should return BuyOrderResponse with an ID.
        [Fact]
        public async void CreateBuyOrder_ProperBuyOrderRequest()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = CreateBuyOrderRequest();
            BuyOrderResponse buyOrderResponse = new BuyOrderResponse();

            // Act
            buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            // Assert
            Assert.True(buyOrderResponse.BuyOrderId != Guid.Empty);
        }
        #endregion

        #region CreateSellOrder
        // If we supply a null parameter, it should throw ArgumentNullException.
        [Fact]
        public void CreateSellOrder_NullRequest()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply Quantity less than 1, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_QuantityLessMinimum()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.Quantity = 0;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply Quantity higher than 100_000, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_QuantityHigherMaximum()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.Quantity = 100_001;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply Price less than 1, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_PriceLessMinimum()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.Price = 0;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply Price higher than 10_000, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_PriceHigherMaximum()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.Price = 10_001;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply StockSymbol as null, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_NullStockSymbol()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.StockSymbol = null;

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply DateAndTimeOfOrder before minimum, it should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_DateAndTimeOfOrderBeforeMinimum()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            sellOrderRequest.DateAndTimeOfOrder = new DateTime(1999, 12, 31);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply a proper sellOrderRequest, it should return SellOrderResponse with an ID.
        [Fact]
        public async void CreateSellOrder_PropersellOrderRequest()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = CreateSellOrderRequest();
            SellOrderResponse sellOrderResponse = new SellOrderResponse();

            // Act
            sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            // Assert
            Assert.True(sellOrderResponse.SellOrderId != Guid.Empty);
        }
        #endregion
    }
}
