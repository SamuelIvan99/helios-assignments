using System.Threading.Tasks;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IJAOCsvProcessor
    {
        Task<Task> ProcessCsv(string filePath);
    }
}
