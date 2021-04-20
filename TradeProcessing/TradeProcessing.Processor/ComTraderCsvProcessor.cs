using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TradeProcessing.DataAceess;
using TradeProcessing.Helpers;

namespace TradeProcessing.Processor
{
    public class ComTraderCsvProcessor : IComTraderCsvProcessor
    {
        readonly HeliosContext _context;
        readonly IEpexParser _parser;

        public ComTraderCsvProcessor(HeliosContext context, IEpexParser epexParser)
        {
            _context = context;
            _parser = epexParser;
        }

        public async Task<Task> ProcessCsv(string csv, Dictionary<string, string> attr)
        {
            var path = attr["FilePath"];
            var machine = attr["MachineName"];
            string key = path + "-" + machine;

            var epexRows = _parser.Parse(csv, CultureInfo.GetCultureInfo("en-US"));

            var posHelp = new PositionsHelper(_context, key);
            var currentPos = await posHelp.GetCurrentPosition();

            var tradeNo = Int32.MinValue;
            foreach (var row in epexRows.OrderBy(r => r.TradeNo))
            {
                tradeNo = Convert.ToInt32(row.TradeNo);
                if (tradeNo > currentPos)
                {
                    // Quantity bestemmes af buy/sell: Hvis vi har købt skal quantity angives med positivt fortegn. 
                    // Hvis vi har solgt skal quantity angives med negativt fortegn
                    decimal quant = row.Quantity;
                    decimal price = row.Price;
                    int fact = row.BuySell.ToUpper() == "B" ? 1 : -1;

                    Effect effect = Effect.FromMegawatts(quant * fact);
                    Money money = new Money(price, new Currency(row.Currency));

                    // Hent alias og heraf grid ud fra landekoden i CSV filen (aflæses i EIC feltet).
                    TsoAlias alias = GetTsoAliases(row.EIC.Substring(3, 2), "TsoAlias");
                    Grid grid = alias.Grid;

                    // Vi henter tidszonen fra grid
                    var timeZone = grid.TimeZoneInfo;

                    //CSV timeperiod format : 20200123 15:00-20200123 15:30
                    DateTimeOffset from = GetEpexDateTimeOffSet(row.Contract.Substring(0, 14), timeZone);
                    DateTimeOffset to = GetEpexDateTimeOffSet(row.Contract.Substring(15, 14), timeZone);
                    TimePeriod timePeriod = new TimePeriod(from, to);

                    var sourceBook = _context.Books.Where(tso => (tso.ShortName == row.TSO || tso.ShortName == row.TSO + " INTRA")).FirstOrDefault() ?? throw new ArgumentException($"Could not get exchange book for TSO {row.TSO}");
                    var targetBook = grid.DefaultTradeBook ?? throw new ArgumentException($"Could not get default trade book for grid {grid}");

                    var traderId = _context.Traders.Where(trader => trader.ExternalTraderId == row.TraderId && trader.TradingPlatform == "EPEX").FirstOrDefault() ?? throw new ArgumentException($"Could not validate trader {row.TraderId}");

                    var transaction = Model.Entities.Transaction.CreateTrade(grid, timePeriod, sourceBook, targetBook, effect, money, traderId.ExternalTraderId, row.Text);
                    transaction.SetExternalReference(new ExternalReference("EPEX-" + row.TSO + "-" + row.OrderNo + "-" + row.TradeNo));

                    var ex = await _context.Transactions.FindByExternalReferenceAsync(transaction.ExternalReference);
                    if (ex == null)
                    {
                        await _context.Transactions.AddAsync(transaction);
                    }
                }
            }
            await posHelp.SetPosition(tradeNo);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public object GetValue(CultureInfo culture, string str) => Date.FromDateTime(DateTime.ParseExact(str, "yyyyMMdd", culture));

        private static DateTimeOffset GetEpexDateTimeOffSet(String epexDateTime, TimeZoneInfo timeZone)
        {
            //CSV epexDateTime format : 20200123 15:00
            // 1. Vi danner dateTime (dato + timer/minutter/sekunder/millisekunder)
            DateTime dateTime = DateTime.ParseExact(epexDateTime.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

            // 2. Vi danner dateTimeOffset ud fra DateTime samt de offset vores grid/market country definerer
            var offset_start = new DateTimeOffset(dateTime, timeZone.GetUtcOffset(dateTime));

            // 3. Justere DateTime med start tidspunkt på dagen og danner TimePeriod
            int hour = GetTimeSlot(epexDateTime.Substring(9, 2), "hour");
            int minute = GetTimeSlot(epexDateTime.Substring(12, 2), "minute");
            return offset_start.At(TimeSpan.FromHours(hour), timeZone).AddMinutes(minute);
        }

        private static int GetTimeSlot(String value, String type)
        {
            try
            {
                return Int16.Parse(value);
            }
            catch (Exception exception)
            {
                throw new ApplicationException($"Could not convert type {type} of value {value} to integer", exception);
            }
        }

        private TsoAlias GetTsoAliases(String value, String type)
        {
            try
            {
                return _context.TsoAliases.Where(tso => tso.ShortName == value).First();
            }
            catch (Exception exception)
            {
                throw new ApplicationException($"Could not convert type {type} of value {value} to TsoAlias", exception);
            }
        }
    }
}
