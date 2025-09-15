using System.ComponentModel.DataAnnotations;

namespace StockApp.Models
{
    /// <summary>
    /// Domain model for BuyOrder.
    /// </summary> 
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderId {  get; set; }
        
        [Required]
        [StringLength(20)]
        public string? StockSymbol { get; set; }

        [Required]
        [StringLength(20)]
        public string? StockName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint? Quantity { get; set; }

        [Range(1, 10000)]
        public double? Price { get; set; }
    }
}
