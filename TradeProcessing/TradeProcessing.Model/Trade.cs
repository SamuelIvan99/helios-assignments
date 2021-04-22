using Semicolon.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TradeProcessing.Model
{
    public class Trade
    {
        [CsvColumn("Trader")]
        public string TraderId { get; set; }

        [CsvColumn("TSO name")]
        public string TSO { get; set; }

        [CsvColumn("EIC")]
        public string EIC { get; set; }

        [CsvColumn("B/S")]
        public string BuySell { get; set; }

        [CsvColumn("Product")]
        public string Product { get; set; }

        [CsvColumn("Contract type")]
        public string Contract { get; set; }

        [CsvColumn("Qantity")]
        public decimal Quantity { get; set; }

        [CsvColumn("Price")]
        public decimal Price { get; set; }

        [CsvColumn("Curr")]
        public string Currency { get; set; }

        [CsvColumn("Act")]
        public string Act { get; set; }

        [CsvColumn("Text")]
        public string Text { get; set; }

        [CsvColumn("State")]
        public string State { get; set; }

        [CsvColumn("Order No")]
        public string OrderNo { get; set; }

        [Key]
        [CsvColumn("Trade No")]
        public string TradeNo { get; set; }

        [CsvColumn("P/O")]
        public string P_O { get; set; }

        [CsvColumn("Date/Time")]
        public string DateTime { get; set; }

        [CsvColumn("BG")]
        public string BG { get; set; }
    }
}