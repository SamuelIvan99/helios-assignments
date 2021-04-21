using Semicolon;
using Semicolon.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.Processor
{
    public class EpexParser : IEpexParser
    {
        public IReadOnlyList<EpexRow> Parse(string csv, CultureInfo culture)
        {
            //var parser = new Parser<EpexRow>(new Options
            //{
            //    CultureInfo = culture,
            //    ColumnSeparator = ';'
            //});
            //csv = csv.Replace("\"", "");
            //csv = csv.Replace(",", ".");

            //IEnumerable<string> onlyHeaderAndText = csv.SplitIntoLines(removeEmptyLines: true)
            //                .SkipWhile(line => line.StartsWith("OT"));
            //var trimmedCsv = onlyHeaderAndText.JoinLines();

            //return parser
            //    .ParseCsv(trimmedCsv)
            //    .ToList();
            throw new NotImplementedException();
        }

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
            }

            var parser = new Parser<EpexRowJao>(new Options
            {
                CultureInfo = culture,
                ColumnSeparator = ';'
            });
            csv = csv.Replace("\"", "");
            csv = csv.Replace(",", ".");

            return parser.ParseCsv(csv).ToList();
        }

        public class EpexRow
        {
            [CsvColumn("Trader Id")]
            public string TraderId { get; set; }

            [CsvColumn("TSO")]
            public string TSO { get; set; }

            [CsvColumn("EIC")]
            public string EIC { get; set; }

            [CsvColumn("B/S")]
            public string BuySell { get; set; }

            [CsvColumn("Product")]
            public string Product { get; set; }

            [CsvColumn("Contract")]
            public string Contract { get; set; }

            [CsvColumn("Qty")]
            public decimal Quantity { get; set; }

            [CsvColumn("Prc")]
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

            [CsvColumn("AggrInd")]
            public string AggrInd { get; set; }

            [CsvColumn("Self")]
            public string Self { get; set; }
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

            [CsvColumn("Quantity")]
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

            [CsvColumn("EIC")]
            public string BG { get; set; }
        }
    }
}
