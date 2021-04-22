using Semicolon;
using Semicolon.Attributes;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.Processor
{
    public class EpexParser : IEpexParser
    {
        public IReadOnlyList<EpexRowJao> ParseJao(string filePath, CultureInfo culture)
        {
            using TextReader reader = new StreamReader(filePath);
            string current, csv = "";
            while ((current = reader.ReadLine()) != null)
            {
                if (current.StartsWith("OT") || current.StartsWith("JAO"))
                {
                    continue;
                }
                csv += reader.ReadLine();
                csv += "\n";
            }

            var parser = new Parser<EpexRowJao>(new Options
            {
                CultureInfo = culture,
                ColumnSeparator = ','
            });
            //csv = csv.Replace("\"", "");
            //csv = csv.Replace(",", ".");

            return parser.ParseCsv(csv).ToList();
        }

        public class EpexRowJao
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
}
