using StockApp.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Contracts.DTOs
{
    public class BuyOrderRequest
    {
        [Required(ErrorMessage = "Stock Symbol is required.")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Names is required.")]
        public string? StockName { get; set; }

        [MinimumDate(2000, 1, 1)]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100_000, ErrorMessage = "Quantity should be between {0} and {1}.")]
        public uint Quantity { get; set; }

        [Range(1, 10_000, ErrorMessage = "Price should be between {0} and {1}.")]
        public double Price { get; set; }
    }
}
