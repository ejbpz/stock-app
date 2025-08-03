using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models.Options
{
    public class TradingOptions
    {
        public string? DefaultStockSymbol { get; set; }
        public string? FinnhubConnection { get; set; }
        public string? ApiKey { get; set; }
    }
}
