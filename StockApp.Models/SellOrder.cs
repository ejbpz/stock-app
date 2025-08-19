using System.ComponentModel.DataAnnotations;

namespace StockApp.Models
{
    public class SellOrder
    {
        public Guid SellOrderId { get; set; }

        [Required(ErrorMessage = "Stock Symbol is required.")]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Names is required.")]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100_000, ErrorMessage = "Quantity should be between {0} and {1}")]
        public uint Quantity { get; set; }

        [Range(1, 10_000, ErrorMessage = "Price should be between {0} and {1}")]
        public double Price { get; set; }
    }
}
