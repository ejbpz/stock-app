using Xunit.Abstractions;
using Moq;
using AutoFixture;
using StockApp.Services;
using StockApp.Contracts.DTOs;
using StockApp.RepositoryContracts;
using FluentAssertions;
using StockApp.Models;

namespace StockApp.Test
{
    public class StocksServiceTest
    {
        private readonly Fixture _fixture;

        private readonly StocksGetterService _stocksGetterService;
        private readonly StocksAdderService _stocksAdderService;

        private readonly Mock<IStocksRepository> _mockStocksRepository;
        private readonly IStocksRepository _stocksRepository;

        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();

            _mockStocksRepository = new Mock<IStocksRepository>();
            _stocksRepository = _mockStocksRepository.Object;

            _stocksGetterService = new StocksGetterService(_stocksRepository);
            _stocksAdderService = new StocksAdderService(_stocksRepository);

            _testOutputHelper = testOutputHelper;
        }

        #region CreateBuyOrder
        // If we supply a null parameter, it should throw ArgumentNullException.
        [Fact]
        public void CreateBuyOrder_NullRequest_ShouldThrowArgumentNullException()
        { 
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Act
            Func<Task> action = async () => {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        // If you supply Quantity less than 1, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityLessMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.Quantity, uint.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Quantity higher than 100_000, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityHigherMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.Quantity, uint.MaxValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Price less than 1, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceLessMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.Price, double.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Price higher than 10_000, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceHigherMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.Price, double.MaxValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply StockSymbol as null, should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.StockSymbol, null as string)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply DateAndTimeOfOrder before minimum, it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateAndTimeOfOrderBeforeMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(o => o.DateAndTimeOfOrder, DateTime.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply a proper BuyOrderRequest, it should return BuyOrderResponse with an ID.
        [Fact]
        public async Task CreateBuyOrder_ProperBuyOrderRequest_ShouldBeSuccessful()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Create<BuyOrderRequest>();
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            BuyOrderResponse buyOrderExpected = buyOrder.ToBuyOrderResponse();

            _mockStocksRepository.Setup(m => m
                .CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            // Act
            var response = await _stocksAdderService.CreateBuyOrder(buyOrderRequest);
            buyOrderExpected.BuyOrderId = response.BuyOrderId;

            // Assert
            response.Should().Be(buyOrderExpected);
            response.BuyOrderId.Should().NotBeEmpty();
            response.BuyOrderId.Should().NotBe(Guid.Empty);
        }
        #endregion

        #region CreateSellOrder
        // If we supply a null parameter, it should throw ArgumentNullException.
        [Fact]
        public void CreateSellOrder_NullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        // If you supply Quantity less than 1, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_QuantityLessMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.Quantity, uint.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Quantity higher than 100_000, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_QuantityHigherMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.Quantity, uint.MaxValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Price less than 1, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_PriceLessMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.Price, double.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply Price higher than 10_000, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_PriceHigherMaximum_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.Price, double.MaxValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply StockSymbol as null, should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.StockSymbol, null as string)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply DateAndTimeOfOrder before minimum, it should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_DateAndTimeOfOrderBeforeMinimum_ShouldThrowArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(o => o.DateAndTimeOfOrder, DateTime.MinValue)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply a proper sellOrderRequest, it should return SellOrderResponse with an ID.
        [Fact]
        public async Task CreateSellOrder_ProperSellOrderRequest_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Create<SellOrderRequest>();
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            SellOrderResponse sellOrderExpected = sellOrder.ToSellOrderResponse();

            _mockStocksRepository.Setup(m => m
                .CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            // Act
            var response = await _stocksAdderService.CreateSellOrder(sellOrderRequest);
            sellOrderExpected.SellOrderId = response.SellOrderId;

            // Assert
            response.Should().Be(sellOrderExpected);
            response.SellOrderId.Should().NotBeEmpty();
            response.SellOrderId.Should().NotBe(Guid.Empty);
        }
        #endregion

        #region GetBuyOrders
        // When we call all the orders, by default should be empty.
        [Fact]
        public async Task GetBuyOrders_EmptyList_ToBeEmpty()
        {
            // Arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            _mockStocksRepository.Setup(m => m
                .GetBuyOrders())
                .ReturnsAsync(buyOrders);

            // Act
            var response = await _stocksGetterService.GetBuyOrders();

            // Assert
            response.Should().BeEmpty();
            response.Should().BeAssignableTo<List<BuyOrderResponse>>();
        }

        // When we call all the orders, returning all added.
        [Fact]
        public async Task GetBuyOrders_ContainsOrdersList_ToBeSuccessful()
        {
            // Arrange
            List<BuyOrder> buyOrders = _fixture.Create<List<BuyOrder>>();
            List<BuyOrderResponse> ordersExpected = buyOrders.Select(o => o.ToBuyOrderResponse()).ToList();

            _mockStocksRepository.Setup(m => m
                .GetBuyOrders())
                .ReturnsAsync(buyOrders);

            // Act
            var response = await _stocksGetterService.GetBuyOrders();

            // Print
            _testOutputHelper.WriteLine("Expected orders:");
            foreach (BuyOrderResponse order in ordersExpected)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            _testOutputHelper.WriteLine("\nReturned orders:");
            foreach (BuyOrderResponse order in response)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            // Assert
            foreach (BuyOrderResponse order in ordersExpected)
            {
                ordersExpected.Should().Contain(order);
            }

            response.Should().BeAssignableTo<List<BuyOrderResponse>>();
            response.Should().BeEquivalentTo(ordersExpected);
        }
        #endregion

        #region GetSellOrders
        // When we call all the orders, by default should be empty.
        [Fact]
        public async Task GetSellOrders_EmptyList_ToBeEmpty()
        {
            // Arrange 
            List<SellOrder> sellOrders = new List<SellOrder>();

            _mockStocksRepository.Setup(m => m
                .GetSellOrders())
                .ReturnsAsync(sellOrders);

            // Act
            var response = await _stocksGetterService.GetSellOrders();

            // Assert
            response.Should().BeEmpty();
            response.Should().BeAssignableTo<List<SellOrderResponse>>();
        }

        // When we call all the orders, returning all added.
        [Fact]
        public async Task GetSellOrders_ContainsOrdersList_ToBeSuccessful()
        {
            // Arrange
            List<SellOrder> sellOrders = _fixture.Create<List<SellOrder>>();
            List<SellOrderResponse> ordersExpected = sellOrders.Select(o => o.ToSellOrderResponse()).ToList();

            _mockStocksRepository.Setup(m => m
                .GetSellOrders())
                .ReturnsAsync(sellOrders);

            // Act
            var response = await _stocksGetterService.GetSellOrders();

            // Print
            _testOutputHelper.WriteLine("Expected orders:");
            foreach (SellOrderResponse order in ordersExpected)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            _testOutputHelper.WriteLine("\nReturned orders:");
            foreach (SellOrderResponse order in response)
            {
                _testOutputHelper.WriteLine(order.ToString());
            }

            // Assert
            foreach (SellOrderResponse order in response)
            {
                ordersExpected.Should().Contain(order);
            }

            response.Should().BeAssignableTo<List<SellOrderResponse>>();
            response.Should().BeEquivalentTo(ordersExpected);
        }
        #endregion
    }
}
