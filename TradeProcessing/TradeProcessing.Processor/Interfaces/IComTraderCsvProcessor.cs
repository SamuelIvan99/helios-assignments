using System.Globalization;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IComTraderCsvProcessor : ICsvProcessor
    {

        object GetValue(CultureInfo culture, string str);
    }
}