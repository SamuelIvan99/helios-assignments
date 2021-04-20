using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TradeProcessing.DataAceess;
using TradeProcessing.Helpers;
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
