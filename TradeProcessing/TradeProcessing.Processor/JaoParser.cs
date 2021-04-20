using System;
using System.Collections.Generic;
using System.Globalization;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.Processor
{
    class JaoParser : IEpexParser
    {
        public IReadOnlyList<EpexParser.EpexRow> Parse(string csv, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
