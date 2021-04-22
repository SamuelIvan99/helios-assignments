using System.Collections.Generic;
using System.Globalization;
using static TradeProcessing.Processor.EpexParser;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IEpexParser
    {
        IReadOnlyList<EpexRowJao> ParseJao(string filePath, CultureInfo culture);
    }
}