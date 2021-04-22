using System.Collections.Generic;
using System.Globalization;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IEpexParser
    {
        IReadOnlyList<EpexRowJao> ParseJao(string filePath, CultureInfo culture);
    }
}