using System.Globalization;
using System.Threading.Tasks;
using TradeProcessing.DataAceess;
using TradeProcessing.Model;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.Processor
{
    public class JAOCsvProcessor : IJAOCsvProcessor
    {
        readonly HeliosContext _context;
        readonly IEpexParser _parser;

        public JAOCsvProcessor(HeliosContext context, IEpexParser epexParser)
        {
            _context = context;
            _parser = epexParser;
        }

        public async Task<Task> ProcessCsv(string filePath)
        {
            var epexRowsJao = _parser.ParseJao(filePath, CultureInfo.GetCultureInfo("en-US"));

            foreach (var row in epexRowsJao)
            {
                Trade trade = _context.Trades.Find(row.TradeNo);

                if (trade == null)
                {
                    await _context.Trades.AddAsync(row);
                }
            }

            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
