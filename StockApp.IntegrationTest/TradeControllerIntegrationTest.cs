using HtmlAgilityPack;
using FluentAssertions;
using Fizzler.Systems.HtmlAgilityPack;

namespace StockApp.Test
{
    public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        #region Index
        [Fact]
        public async Task Index_ToReturnView()
        {
            // Arrange
            HttpClient _client = _factory.CreateClient();
            
            // Act
            HttpResponseMessage response = await _client.GetAsync("/trade/");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            string responseBody = await response.Content.ReadAsStringAsync();
            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(responseBody);
            var document = htmlDocument.DocumentNode;

            document.QuerySelector("#price").Should().NotBeNull();
            Convert.ToDouble(document.QuerySelector("#price").InnerText.ToString()).Should().NotBeNaN();
        }
        #endregion 
    }
}
