using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeProcessing.Processor.Interfaces
{
    public interface ICsvProcessor
    {
        Task<Task> ProcessCsv(string csv, Dictionary<string, string> attr);
    }
}
