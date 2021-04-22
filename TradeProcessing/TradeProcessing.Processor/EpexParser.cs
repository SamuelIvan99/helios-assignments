using Semicolon;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TradeProcessing.Model;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.Processor
{
    public class EpexParser : IEpexParser
    {
        public IReadOnlyList<Trade> ParseJao(string filePath, CultureInfo culture)
        {
            using TextReader reader = new StreamReader(filePath);
            string current, csv = "";
            while ((current = reader.ReadLine()) != null)
            {
                if (current.StartsWith("OT") || current.StartsWith("JAO") || current.StartsWith(",,"))
                {
                    continue;
                }
                csv += current;
                csv += "\n";
            }

            var parser = new Parser<Trade>(new Options
            {
                CultureInfo = culture,
                ColumnSeparator = ','
            });

            return parser.ParseCsv(csv).ToList();
        }
    }
}
