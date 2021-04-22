using System.Collections.Generic;
using System.Globalization;
using TradeProcessing.Model;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IEpexParser
    {
        IReadOnlyList<Trade> ParseJao(string filePath, CultureInfo culture);
    }
}