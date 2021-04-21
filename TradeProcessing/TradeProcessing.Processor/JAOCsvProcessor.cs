using System.Globalization;
using System.Linq;
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

            foreach (var row in epexRowsJao.OrderBy(r => r.TradeNo))
            {
                Trade trade = _context.Trades.Find(row.TradeNo);

                if (trade == null)
                {
                    trade = new Trade
                    {
                        Act = row.Act,
                        BG = row.BG,
                        BuySell = row.BuySell,
                        Contract = row.Contract,
                        Currency = row.Currency,
                        DateTime = row.DateTime,
                        EIC = row.EIC,
                        OrderNo = row.OrderNo,
                        Price = row.Price,
                        Product = row.Product,
                        P_O = row.P_O,
                        Quantity = row.Quantity,
                        State = row.State,
                        Text = row.Text,
                        TradeNo = row.TradeNo,
                        TraderId = row.TraderId,
                        TSO = row.TSO
                    };
                    await _context.Trades.AddAsync(trade);
                }
            }

            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
