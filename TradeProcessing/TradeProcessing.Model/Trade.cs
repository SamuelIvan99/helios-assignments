using System.ComponentModel.DataAnnotations;

namespace TradeProcessing.Model
{
    public class Trade
    {
        public string TraderId { get; set; }

        public string TSO { get; set; }

        public string EIC { get; set; }

        public string BuySell { get; set; }

        public string Product { get; set; }

        public string Contract { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public string Act { get; set; }

        public string Text { get; set; }

        public string State { get; set; }

        public string OrderNo { get; set; }

        [Key]
        public string TradeNo { get; set; }

        public string P_O { get; set; }

        public string DateTime { get; set; }

        public string BG { get; set; }
    }
}
