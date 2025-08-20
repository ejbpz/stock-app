using StockApp.Contracts.DTOs;
using StockApp.Services;
using Xunit.Abstractions;

namespace StockApp.Test
{
    public class StocksServiceTest
    {
        private readonly StocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stocksService = new StocksService();
            _testOutputHelper = testOutputHelper;
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

        private List<BuyOrderRequest> CreateListBuyOrderRequest()
        {
            return new List<BuyOrderRequest>()
            {
                new BuyOrderRequest()
                {
                    Quantity = 23,
                    Price = 340.34,
                    StockSymbol = "MSFT",
                    StockName = "Microsoft Corp",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new BuyOrderRequest()
                {
                    Quantity = 4576,
                    Price = 7887.6,
                    StockSymbol = "IBM",
                    StockName = "International Business Machines Corp",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new BuyOrderRequest()
                {
                    Quantity = 345,
                    Price = 1255,
                    StockSymbol = "AAPL",
                    StockName = "Apple Inc",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new BuyOrderRequest()
                {
                    Quantity = 1234,
                    Price = 877.34,
                    StockSymbol = "INTC",
                    StockName = "Intel Corporation",
                    DateAndTimeOfOrder = DateTime.Now,
                },
            };
        }

        private List<SellOrderRequest> CreateListSellOrderRequest()
        {
            return new List<SellOrderRequest>()
            {
                new SellOrderRequest()
                {
                    Quantity = 788,
                    Price = 8452,
                    StockSymbol = "AMZN",
                    StockName = "Amazon.com, Inc",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new SellOrderRequest()
                {
                    Quantity = 74,
                    Price = 879.33,
                    StockSymbol = "META",
                    StockName = "Meta Platforms, Inc",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new SellOrderRequest()
                {
                    Quantity = 1110,
                    Price = 963,
                    StockSymbol = "ABNB",
                    StockName = "Airbnb, Inc",
                    DateAndTimeOfOrder = DateTime.Now,
                },
                new SellOrderRequest()
                {
                    Quantity = 9999,
                    Price = 4744,
                    StockSymbol = "EBAY",
                    StockName = "eBay Inc",
                    DateAndTimeOfOrder = DateTime.Now,
                },
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
            Assert.ThrowsAsync<ArgumentNullException>(async () => { 
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

        #region GetBuyOrders
        // When we call all the orders, by default should be empty.
        [Fact]
        public async void GetBuyOrders_EmptyList()
        {
            // Arrange 
            List<BuyOrderResponse> buyOrdersList = new List<BuyOrderResponse>();

            // Act
            buyOrdersList = await _stocksService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrdersList);
        }

        // When we call all the orders, returning all added.
        [Fact]
        public async void GetBuyOrders_ContainsOrdersList()
        {
            // Arrange
            List<BuyOrderRequest> ordersToAdd = CreateListBuyOrderRequest();
            List<BuyOrderResponse> buyOrdersAdded = new List<BuyOrderResponse>();
            List<BuyOrderResponse> allOrders = new List<BuyOrderResponse>();

            // Act
            foreach (BuyOrderRequest buyOrder in ordersToAdd)
            {
                buyOrdersAdded.Add(await _stocksService.CreateBuyOrder(buyOrder));
            }

            allOrders = await _stocksService.GetBuyOrders();

            // Print
            _testOutputHelper.WriteLine("Expected orders:");
            foreach (BuyOrderResponse order in buyOrdersAdded)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            _testOutputHelper.WriteLine("\nCurrent orders:");
            foreach (BuyOrderResponse order in allOrders)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            // Assert
            foreach (BuyOrderResponse order in buyOrdersAdded)
            {
                Assert.Contains(order, allOrders);
            }
        }
        #endregion

        #region GetSellOrders
        // When we call all the orders, by default should be empty.
        [Fact]
        public async void GetSellOrders_EmptyList()
        {
            // Arrange 
            List<SellOrderResponse> sellOrdersList = new List<SellOrderResponse>();

            // Act
            sellOrdersList = await _stocksService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrdersList);
        }

        // When we call all the orders, returning all added.
        [Fact]
        public async void GetSellOrders_ContainsOrdersList()
        {
            // Arrange
            List<SellOrderRequest> ordersToAdd = CreateListSellOrderRequest();
            List<SellOrderResponse> sellOrdersAdded = new List<SellOrderResponse>();
            List<SellOrderResponse> allOrders = new List<SellOrderResponse>();

            // Act
            foreach (SellOrderRequest sellOrder in ordersToAdd)
            {
                sellOrdersAdded.Add(await _stocksService.CreateSellOrder(sellOrder));
            }

            allOrders = await _stocksService.GetSellOrders();

            // Print
            _testOutputHelper.WriteLine("Expected orders:");
            foreach (SellOrderResponse order in sellOrdersAdded)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            _testOutputHelper.WriteLine("\nCurrent orders:");
            foreach (SellOrderResponse order in allOrders)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            // Assert
            foreach (SellOrderResponse order in sellOrdersAdded)
            {
                Assert.Contains(order, allOrders);
            }
        }
        #endregion
    }
}
