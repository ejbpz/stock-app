using StockApp.Models;
using StockApp.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Contracts.DTOs
{
    /// <summary>
    /// DTO Request to buy an order.
    /// </summary>
    public class BuyOrderRequest
    {
        [Required(ErrorMessage = "Stock Symbol is required.")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Names is required.")]
        public string? StockName { get; set; }

        [MinimumDate(2000, 1, 1, ErrorMessage = "The date given is not valid.")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Quantity should be between {0} and {1}.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Price should be between {0} and {1}.")]
        public double Price { get; set; }

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                Price = Price,
                Quantity = Quantity,
                StockName = StockName,
                StockSymbol = StockSymbol,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
            };
        }
    }
}
