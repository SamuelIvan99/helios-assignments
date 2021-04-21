using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IComTraderCsvProcessor
    {
        Task<Task> ProcessCsv(string csv, Dictionary<string, string> attr);

        object GetValue(CultureInfo culture, string str);
    }
}